
# 🧑‍💻 AssistantManager – Copilot Prompt Starter Kit (Updated with Unity)

This starter kit provides modular prompts for GitHub Copilot to scaffold the AssistantManager app.  
It now incorporates **Unity for 3D drill authoring** and **web playback via Three.js/Babylon.js**, while keeping matchday focused on squad, substitutions, and stats.

---

## 📑 Prompt Index

| **Category**            | **File / Module**              | **Prompt Purpose**                                                                 |
|--------------------------|--------------------------------|-------------------------------------------------------------------------------------|
| 🧱 Project Setup         | Solution & Projects            | Create VS2026 solution with Client, Server, Shared projects; .NET 10; GitHub Pages   |
| 🧩 Core Models           | Player.cs                      | Player details, position preferences, injuries, availability, coach ratings         |
|                          | Event.cs                       | Event details, type, location, notes, availability list                             |
|                          | PositionPreference.cs          | Position code + comfort level                                                       |
|                          | InjuryRecord.cs                | Injury start/end dates, notes                                                       |
|                          | CoachRating.cs                 | PlayerId, PositionCode, Rating                                                      |
|                          | SuspensionRecord.cs            | Suspension tracking: PlayerId, EventId, Reason, MatchesRemaining                    |
|                          | Drill.cs                       | Name, Description, Category, Duration, UnitySceneReference, PlaybackScript, Participants |
| 🟥 Suspension Logic      | SuspensionRules.cs             | IFAB/NSFA rules: red cards, yellows, resets, carry-over, friendly match exclusions  |
| 🧠 ViewModels            | SquadSelectionViewModel.cs     | FieldZones, BenchPlayers, DraggedPlayer; drag/drop + auto-suggest methods           |
|                          | EventDetailViewModel.cs        | CurrentEvent, Availability list; load, update, save methods                         |
| 🧱 UI Pages              | SquadSelection.razor           | Drag-and-drop squad selection with auto-suggest and lockable zones                  |
|                          | EventDetail.razor              | Event form + player availability toggles                                            |
|                          | Matchday.razor                 | Matchday dashboard: squad, formation, subs, goals, cards, injuries, suspension icons|
| 🧰 Components            | FieldZone.razor                | Drop target for player cards; supports locking                                      |
|                          | BenchList.razor                | Draggable list of unassigned players                                                |
|                          | PlayerCard.razor               | Compact player info with availability/suspension indicators                         |
| 📚 Training Module       | DrillLibrary.cs                | Manage drill metadata, link Unity exports, store JSON definitions                   |
|                          | TrainingPlanner.razor          | Build sessions from drill library, track attendance, export printable sheets        |
|                          | DrillViewer.razor              | Lightweight 3D playback using Three.js/Babylon.js                                   |
| 🔌 Interop Layer (Client/Interop) | UnityInterop.js       | Embed Unity drill editor in Blazor (desktop only), export JSON                     |
|                          | PlaybackInterop.js             | Load/play Unity-exported JSON in Three.js/Babylon.js; touch-friendly controls       |
|                          | InteropHelpers.cs              | Strongly typed C# wrappers for JSRuntime calls (LoadUnityEditor, PlayDrill, etc.)  |
| 🔌 API Controllers       | PlayersController.cs           | CRUD for players                                                                    |
|                          | EventsController.cs            | CRUD for events                                                                     |
|                          | AvailabilityController.cs      | Get/set player availability                                                         |
|                          | SquadController.cs             | POST /api/squad/suggest → suggested FieldZones + Bench list                         |
| 🧪 Testing               | SquadBuilderService Tests      | Unit tests: suggestions, suspension exclusions, ratings, comfort levels             |
|                          | ViewModel Tests                | Unit tests for SquadSelection + EventDetail viewmodels                              |
|                          | Drill Export/Playback Tests    | Validate Unity JSON exports load correctly in web viewer                            |
| 🧱 Deployment & Design   | GitHub Pages                   | Configure Blazor WASM for static deployment with offline support                    |
|                          | Responsive Design              | Media queries for iPad Air (5th gen) + 4K desktop                                   |

---

## 🧱 Project Setup

```plaintext
// Create a Visual Studio 2026 solution named AssistantManager.
// Add three projects: Client (Blazor WebAssembly), Server (ASP.NET Core Web API), Shared (class library).
// Use .NET 10 and configure for GitHub Pages deployment.
// Target framework: net10.0
// UnityEditor project is separate from Blazor solution. Export drills as JSON for playback.
```

---

## 🧩 Core Models

### Player.cs

```csharp
// Define Player with personal details, position preferences, injury history, availability, and coach ratings.
```

### Event.cs

```csharp
// Define Event with EventDate, EventType (enum), Location, Notes, and player availability list.
```

### PositionPreference.cs

```csharp
// Define PositionPreference with PositionCode (e.g. "CB") and ComfortLevel (1–10).
```

### InjuryRecord.cs

```csharp
// Define InjuryRecord with StartDate, EndDate, Notes.
```

### CoachRating.cs

```csharp
// Define CoachRating with PlayerId, PositionCode, Rating (0–10).
```

### SuspensionRecord.cs

```csharp
// Define SuspensionRecord with PlayerId, EventId, Reason ("Red Card", "Accumulated Yellows"), MatchesRemaining.
```

### Drill.cs

```csharp
// Define Drill with Name, Description, Category (Passing, Defending, etc.), Duration, UnitySceneReference (for editor), PlaybackScript (for Three.js/Babylon.js), and a list of Participants.
// For the player list implement a lightweight reference (e.g. List<Guid> PlayerIds) in the Shared project to avoid heavy coupling between drills and players
```

---

## 🟥 Suspension Logic

### SuspensionRules.cs

```csharp
// Implement suspension logic:
// - Red card = 1+ match suspension
// - Second yellow = red card logic
// - 5, 8, 11 yellows = 1-match suspension
// - Reset yellow count after season
// - Carry over unserved suspensions
// - Friendly matches excluded unless rules allow
```

---

## 🧠 ViewModels

### SquadSelectionViewModel.cs

```csharp
// Create SquadSelectionViewModel with FieldZones, BenchPlayers, DraggedPlayer.
// Methods: InitializeAsync(), StartDrag(), DropPlayer(), AutoSuggestAsync().
```

### EventDetailViewModel.cs

```csharp
// Create EventDetailViewModel with CurrentEvent and Availability list.
// Methods: LoadEvent(), UpdateAvailability(), SaveEvent().
```

---

## 🧱 UI Pages

### SquadSelection.razor

```razor
// Drag-and-drop squad selection page with field zones, bench list, auto-suggest, and lockable zones.
```

### EventDetail.razor

```razor
// Event detail page with event form and player availability toggles.
```

### Matchday.razor

```razor
// Create a matchday dashboard.
// Display squad and formation.
// Track substitutions, minutes played, goals, cards, injuries.
// Show suspension indicators (🟥, 🟨, ⛔).
// No drills or training checklists here.
```

---

## 🧰 Components

### FieldZone.razor

```razor
// Drop target for a field position. Accepts PlayerCard via drag-and-drop. Supports locking.
```

### BenchList.razor

```razor
// Draggable list of unassigned players. Supports drag-and-drop into FieldZone.
```

### PlayerCard.razor

```razor
// Compact player info card showing availability, suspension, and override indicators.
```

---

## 📚 Training Module Prompts (Unity Integration)

### DrillLibrary.cs

```csharp
// Create DrillLibrary class to manage metadata for drills.
// Link Unity-exported JSON files containing drill definitions.
// Support CRUD operations for drills and sessions.
```

### TrainingPlanner.razor

```razor
// Create a training planner page.
// Allow coaches to combine drills into sessions.
// Track attendance and export printable sheets.
// Show drill metadata and notes (no playback here).
```

### DrillViewer.razor

```razor
// Create a lightweight 3D drill viewer component.
// Use Three.js or Babylon.js to render Unity-exported JSON drill data.
// Touch-friendly for iPad Air (5th gen).
```

---

## 🔌 Interop Layer (Client/Interop)

### UnityInterop.js

```javascript
// Provide functions to embed the Unity drill editor in a Blazor page (desktop only).
// Handle initialization, scene loading, and export of drill JSON.
// Expose methods for saving/exporting drills to wwwroot/Drills.
```

### PlaybackInterop.js

```javascript
// Provide functions to load and control Three.js/Babylon.js drill playback.
// Accept Unity-exported JSON as input.
// Support play, pause, reset, and camera controls.
// Ensure touch-friendly controls for iPad Air (5th gen).
```

### InteropHelpers.cs

```csharp
// Create strongly typed C# wrappers for JSRuntime calls.
// Methods: LoadUnityEditor(), ExportDrillJson(), PlayDrill(), PauseDrill(), ResetDrill(), SetCameraView().
// Bridge Blazor components (DrillViewer.razor, TrainingPlanner.razor) with JS interop functions.
```

---

## 🔌 API Controllers

### PlayersController.cs

```csharp
// REST API for players: GET all, GET by ID, POST, PUT, DELETE.
```

### EventsController.cs

```csharp
// REST API for events: GET all, GET by ID, POST, PUT, DELETE.
```

### AvailabilityController.cs

```csharp
// API to get and set player availability for events.
```

### SquadController.cs

```csharp
// POST /api/squad/suggest
// Returns suggested FieldZones and Bench list based on availability, comfort, injuries, ratings, and suspension status.
```

---

## 🧪 Testing Prompts

### SquadBuilderService Tests

```csharp
// Unit tests for SquadBuilderService:
// - Suggests correct players
// - Excludes suspended players
// - Honors coach ratings and comfort levels
```

### ViewModel Tests

```csharp
// Unit tests for SquadSelectionViewModel and EventDetailViewModel.
// Mock services and test drag/drop, availability updates, and auto-suggestions.
```

### Drill Export/Playback Tests

```csharp
// Write tests to validate Unity-exported drill JSON loads correctly in DrillViewer.
// Ensure playback works on iPad Air and 4K desktop screens.
```

---

## 🧱 Deployment & Responsiveness

### GitHub Pages

```plaintext
// Configure Blazor WebAssembly project for static deployment to GitHub Pages.
// Include manifest.json and service worker for offline support.
```

### Responsive Design

```css
/* Optimize layout for iPad Air (5th gen) and 4K desktop screens */
/* Use media queries to adjust grid, font sizes, and touch targets */
```

---

## 🎯 Kickoff Prompt (Updated)

```plaintext
// Scaffold AssistantManager solution with Client (Blazor WASM), Server (ASP.NET Core Web API), Shared.
// Add Unity integration for 3D drill authoring (desktop).
// Implement DrillLibrary and DrillViewer for training module.
// Use Three.js/Babylon.js for playback on iPad Air.
// Keep Matchday module focused on squad, substitutions, stats, and suspension logic.
// Optimize UI for iPad Air (5th gen) and 4K desktop screens.
```

---
