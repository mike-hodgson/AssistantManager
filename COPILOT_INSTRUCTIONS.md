# AssistantManager – Blazor WebAssembly Coaching App

## 🧭 Overview

AssistantManager is a Blazor WebAssembly PWA designed for amateur football coaches to manage players, events, squad selection, training, and matchday logistics. It runs entirely client-side with a lightweight ASP.NET Core Web API backend and uses EF Core with SQLite for persistence. The app is optimized for use on both desktop (including 4K screens) and tablet (iPad Air 5th gen), with responsive design and offline support.

---

## 🧱 Architecture

- **Frontend**: Blazor WebAssembly (Client)
- **Backend**: ASP.NET Core Web API (Server)
- **Shared**: DTOs, Enums, and lightweight models
- **Database**: EF Core with SQLite
- **Mapping**: AutoMapper for model-to-DTO conversion
- **Hosting**: GitHub Pages (via Blazor WASM static deployment)
- **Design Pattern**: MVVM (Model-View-ViewModel)

---

## 📦 Project Structure

```plaintext
AssistantManager/
├── Client/                     # Blazor WebAssembly frontend
│   ├── Pages/                  # Routeable .razor pages (SquadSelection, EventDetail, etc.)
│   ├── Components/             # Reusable UI components (FieldZone, BenchList, PlayerCard)
│   ├── ViewModels/             # UI logic and state (SquadSelectionViewModel, etc.)
│   ├── Services/               # API wrappers (PlayerService, EventService)
│   ├── Models/                 # DTOs and UI models
│   └── wwwroot/                # Static assets (CSS, icons, manifest.json)
├── Server/                     # ASP.NET Core backend
│   ├── Controllers/            # REST API endpoints (PlayersController, EventsController)
│   ├── Models/                 # EF Core domain models (Player, Event, etc.)
│   ├── Data/                   # AppDbContext
│   ├── Services/               # Business logic (SquadBuilderService, etc.)
│   └── Mapping/                # AutoMapper profiles
├── Shared/                     # Shared DTOs, Enums, and lightweight models
│   ├── DTOs/                   # PlayerDto, EventDto, etc.
│   ├── Enums/                  # AvailabilityStatus, EventType, PositionCode
│   └── Models/                 # FieldZone, CoachRating, etc.
├── Tests/                      # Unit and integration tests
│   ├── Client.Tests/           # ViewModel and UI logic tests
│   ├── Server.Tests/           # API and service layer tests
│   └── Shared.Tests/           # DTO and mapping tests
```

---

## 🧩 Core Models

### Player

- FirstName, Surname, Nickname, Phone, Address, DateOfBirth, RegistrationNumber
- `List<PositionPreference>`: preferred positions and comfort levels
- `List<InjuryRecord>`: injury history
- `List<PlayerAvailability>`: availability per event
- `List<CoachRating>`: coach-assigned ratings per position

### Event

- EventDate, EventType (Training, Match), Location, Notes
- `List<PlayerAvailability>`: per-player status

### PositionPreference

- PositionCode (e.g. "CB", "ST"), ComfortLevel (1–10)

### InjuryRecord

- StartDate, EndDate, Notes

### PlayerAvailability

- PlayerId, EventId, Status (Available, Unavailable, Maybe), Notes

### CoachRating

- PlayerId, PositionCode, Rating (0–10)

### FieldZone (UI-only)

- Label (e.g. "LB"), ZoneGroup (e.g. "Defense"), Player, IsLocked

---

## 🔄 Feature Modules

### 1. Squad Selection

- Drag/drop players into field zones
- Auto-suggest based on availability, comfort, injuries, coach ratings
- Lock zones to prevent overwrite
- Bench list for unassigned players

### 2. Event Management

- Create/edit training and match events
- Assign player availability
- Filter by date, type, location

### 3. Player Profiles

- View/edit player details
- Track injury history
- Set position preferences
- Assign coach ratings per position

### 4. Training Planner

- Create training sessions with drills
- Track attendance
- Export printable session plans

### 5. Matchday Dashboard

- View selected squad and formation
- Track substitutions and minutes played
- Mark goals, cards, injuries

### 6. Season Stats

- Attendance summaries
- Injury tracking
- Minutes played, goals, cards
- Position usage and rating trends

---

## 🟥 Suspension Logic

### 🔒 Suspension Rules Summary

Based on IFAB Laws of the Game and NSFA competition regulations:

- Red Card: Immediate suspension for minimum 1 match. Additional matches may be added depending on severity.
- Second Yellow in Same Match: Treated as a red card (1-match suspension).
- Accumulated Yellow Cards:
  - 5 yellow cards = 1-match suspension.
  - Additional suspensions at 8 and 11 yellows.
  - Count resets after the regular season unless otherwise specified by competition rules.
- Carry-Over: Suspensions not served before season end carry into the next season.
- Friendly Matches: Do not count toward serving suspensions unless explicitly allowed by competition rules.

### 🧠 Suspension Model

```csharp
public class SuspensionRecord {
    public int PlayerId { get; set; }
    public int EventId { get; set; }
    public string Reason { get; set; } // "Red Card", "Accumulated Yellows", etc.
    public int MatchesRemaining { get; set; }
}
```

### 🔁 Suspension Workflow

1. Post-Match Event Processing
   - Count yellow/red cards.
   - Update SuspensionRecord if thresholds met.
   - Flag suspended players for next match.

2. Squad Suggestion Filtering
   - Exclude players with MatchesRemaining > 0 unless CoachOverride == true.

3. Matchday Dashboard
   - Show suspension indicators (e.g. 🟥, 🟨, ⛔).
   - Prevent assignment to field zones unless overridden.

4. Season Transition
   - Persist unserved suspensions.
   - Reset yellow card counts if competition rules allow.

---

## 🔁 Key Workflows

- Coach creates a new match event → selects available players → auto-suggests squad → locks key positions → prints match card
- Player marked unavailable for training → injury record added → excluded from auto-suggest
- Coach updates player’s comfort level for "CM" → affects future squad suggestions
- Coach assigns a 7/10 rating for a player at "RB" → boosts their priority in auto-suggest
- Training session created → drills added → attendance marked → printable sheet generated

---

## 🔌 API Endpoints

### PlayersController

- `GET /api/players`
- `GET /api/players/{id}`
- `POST /api/players`
- `PUT /api/players/{id}`
- `DELETE /api/players/{id}`

### EventsController

- `GET /api/events`
- `GET /api/events/{id}`
- `POST /api/events`
- `PUT /api/events/{id}`
- `DELETE /api/events/{id}`

### AvailabilityController

- `GET /api/availability/event/{eventId}`
- `POST /api/availability`

### SquadController

- `POST /api/squad/suggest` → returns suggested FieldZones and Bench list

---

## 🧠 ViewModels

### SquadSelectionViewModel

- `List<FieldZone> FieldZones`
- `List<PlayerDto> BenchPlayers`
- `PlayerDto? DraggedPlayer`
- Methods: `InitializeAsync()`, `StartDrag()`, `DropPlayer()`, `AutoSuggestAsync()`

### EventDetailViewModel

- `EventDto CurrentEvent`
- `List<PlayerAvailabilityDto> Availability`
- Methods: `LoadEvent()`, `UpdateAvailability()`, `SaveEvent()`

---

## 🧱 UI Pages

- `SquadSelection.razor`: drag/drop field layout, bench, auto-suggest
- `EventDetail.razor`: event form, availability list
- `PlayerProfile.razor`: player info, injuries, ratings
- `TrainingPlanner.razor`: drill list, attendance
- `Matchday.razor`: live match tracking
- `StatsDashboard.razor`: season summaries

---

## 🧰 Components

- `FieldZone.razor`: drop target for a position
- `BenchList.razor`: draggable list of unassigned players
- `PlayerCard.razor`: compact player display
- `AvailabilityToggle.razor`: status selector for a player
- `DrillCard.razor`: training drill summary

---

## 🧪 Testing & Extensibility

- Use dependency injection for services (e.g. `ISquadService`)
- ViewModels should be unit-testable
- DTOs should be mapped via AutoMapper
- Support offline mode via local storage (future)
- Support print-friendly views for matchday and training

---

## 🔚 Notes

- Use enums for `EventType`, `AvailabilityStatus`, and `PositionCode`
- Use SQLite for local development
- Use GitHub Pages for deployment (Blazor WASM static site)
- Prioritize responsiveness for iPad Air (5th gen) and 4K desktop screens
