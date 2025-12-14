self.addEventListener('install', event =>
{
    console.log('Service worker installing. Pre-caching assets...');
    self.skipWaiting();
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache =>
        {
            // Ensure core navigation page and fallback image are cached
            const toCache = new Set(ASSETS.concat([FALLBACK_HTML, FALLBACK_IMAGE]));
            return cache.addAll(Array.from(toCache));
        })
    );
});

self.addEventListener('activate', event =>
{
    console.log('Service worker activated. Cleaning old caches...');
    event.waitUntil(
        caches.keys().then(keys =>
            Promise.all(
                keys
                    .filter(k => k.startsWith(CACHE_PREFIX) && k !== CACHE_NAME)
                    .map(k => caches.delete(k))
            )
        ).then(() => self.clients.claim())
    );
});

function isNavigationRequest(request)
{
    return request.mode === 'navigate' || (request.headers.get('accept') || '').includes('text/html');
}

self.addEventListener('fetch', event =>
{
    const req = event.request;
    const url = new URL(req.url);

    // Network-first for navigation requests (try network, fallback to cache)
    if (isNavigationRequest(req))
    {
        event.respondWith(
            fetch(req).then(resp =>
            {
                // Update cache for the navigation page
                const copy = resp.clone();
                caches.open(CACHE_NAME).then(cache => cache.put(FALLBACK_HTML, copy)).catch(() => { });
                return resp;
            }).catch(() => caches.match(FALLBACK_HTML))
        );
        return;
    }

    // Network-first for API calls
    if (url.pathname.startsWith('/api/'))
    {
        event.respondWith(
            fetch(req).then(resp =>
            {
                // Optionally cache API responses if needed (left out)
                return resp;
            }).catch(() => new Response(JSON.stringify({ error: 'offline' }), { status: 503, headers: { 'Content-Type': 'application/json' } }))
        );
        return;
    }

    // Cache-first for static assets (from precache list)
    event.respondWith(
        caches.match(req).then(cached =>
        {
            if (cached) return cached;
            return fetch(req).then(networkResp =>
            {
                // Put fetched file in cache for future use if it's a GET and from same-origin
                if (req.method === 'GET' && networkResp && networkResp.ok && url.origin === self.location.origin)
                {
                    const copy = networkResp.clone();
                    caches.open(CACHE_NAME).then(cache => cache.put(req, copy)).catch(() => { });
                }
                return networkResp;
            }).catch(err =>
            {
                // If request is an image, return fallback image
                if (req.destination === 'image' || req.headers.get('accept')?.includes('image'))
                {
                    return caches.match(FALLBACK_IMAGE);
                }
                // As a last resort for other requests, return fallback HTML for navigations handled earlier
                return caches.match(FALLBACK_HTML);
            });
        })
    );
});
