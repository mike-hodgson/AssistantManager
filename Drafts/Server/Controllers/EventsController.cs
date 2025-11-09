[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        return await _context.Events
            .Include(e => e.PlayerAvailability)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var evt = await _context.Events
            .Include(e => e.PlayerAvailability)
            .FirstOrDefaultAsync(e => e.EventId == id);

        if (evt == null) return NotFound();
        return evt;
    }

    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent(Event evt)
    {
        _context.Events.Add(evt);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEvent), new { id = evt.EventId }, evt);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, Event evt)
    {
        if (id != evt.EventId) return BadRequest();
        _context.Entry(evt).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var evt = await _context.Events.FindAsync(id);
        if (evt == null) return NotFound();
        _context.Events.Remove(evt);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}