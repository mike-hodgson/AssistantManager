# Working file - where I'm up to

## Architectural Diagram

```mermaid
flowchart TD

    subgraph Desktop["Desktop (Windows)"]
        U[Unity Editor<br>3D Drill Authoring]
        D[Blazor Desktop/WPF<br>Player & Session Management]
    end

    subgraph Backend["Backend (ASP.NET Core + EF Core)"]
        DB[(Database/Cloud Storage)]
    end

    subgraph Web["Web/iPad (Blazor WebAssembly)"]
        W[Blazor WASM<br>Events, Squad, Training Planner]
        V[DrillViewer<br>Three.js/Babylon.js Playback]
        F[Formation Designer<br>Konva.js/Fabric.js]
        M[Matchday Dashboard<br>Squad, Subs, Stats, Suspensions]
    end

    %% Workflow connections
    U -->|Export Drill JSON| DB
    D -->|Player/Session Data| DB
    DB --> W
    DB --> V
    DB --> F

    W --> M
    W --> V
    W --> F
```

## Dependency Map

```mermaid
flowchart LR

    %% Core foundation
    Foundation[Project Setup\nSolution + Projects + EF Core]
    Models[Core Models\nPlayer, Event, Position, Injury, Rating, Suspension]
    Suspension[Suspension Logic]

    %% Client-side logic
    ViewModels[ViewModels\nSquadSelection + EventDetail]
    UI[UI Pages\nSquadSelection, EventDetail, Matchday]
    Components[Components\nFieldZone, BenchList, PlayerCard]

    %% Training module
    DrillLib[DrillLibrary\nManage Unity exports]
    TrainingPlanner[TrainingPlanner.razor\nCombine drills + attendance]
    DrillViewer[DrillViewer.razor\nPlayback via Three.js/Babylon.js]

    %% Server-side
    API[API Controllers\nPlayers, Events, Availability, Squad]

    %% Testing + Deployment
    Tests[Testing\nUnit + Integration]
    Deploy[Deployment & Responsiveness\nGitHub Pages + iPad/4K UI]

    %% Dependencies
    Foundation --> Models
    Models --> Suspension
    Models --> API
    Suspension --> ViewModels
    Models --> ViewModels
    ViewModels --> UI
    Components --> UI

    DrillLib --> TrainingPlanner
    DrillLib --> DrillViewer
    TrainingPlanner --> UI
    DrillViewer --> UI

    API --> ViewModels
    API --> UI

    UI --> Tests
    API --> Tests
    DrillViewer --> Tests

    Tests --> Deploy
    UI --> Deploy
```

## Executive Summary diagram/map

```mermaid
flowchart LR

    Desktop[Desktop - Windows<br>Unity + Blazor Desktop]
    Backend[Backend<br>ASP.NET Core + EF Core]
    Web[Web/iPad<br>Blazor WASM + JS Viewers]

    Desktop -->|Drill JSON + Player Data| Backend
    Backend --> Web

    Web -->|Training Planner + Drill Viewer + Formation Designer + Matchday| Web
```

## All-in-one diagram/map

```mermaid
flowchart TD

    %% Desktop authoring
    subgraph Desktop["Desktop (Windows)"]
        U[Unity Editor\n3D Drill Authoring]
        D[Blazor Desktop/WPF\nPlayer & Session Management]
    end

    %% Backend
    subgraph Backend["ASP.NET Core Web API + EF Core"]
        DB[(Database / Cloud Storage)]
    end

    %% Web/iPad
    subgraph Web["Web/iPad (Blazor WebAssembly)"]
        W[Blazor WASM\nEvents, Squad, Training Planner]
        V[DrillViewer\nThree.js/Babylon.js Playback]
        F[Formation Designer\nKonva.js/Fabric.js]
        M[Matchday Dashboard\nSquad, Subs, Stats, Suspensions]
    end

    %% Dependencies & workflow
    U -->|Export Drill JSON| DB
    D -->|Player/Session Data| DB
    DB --> W
    DB --> V
    DB --> F

    W --> V
    W --> F
    W --> M

    %% Components feeding UI
    subgraph Components["UI Components"]
        C1[FieldZone.razor]
        C2[BenchList.razor]
        C3[PlayerCard.razor]
    end

    Components --> W
    Components --> M

    %% Suspension logic
    Suspension[Suspension Logic\nIFAB + NSFA Rules]
    Suspension --> W
    Suspension --> M

    %% API controllers
    API[API Controllers\nPlayers, Events, Availability, Squad]
    API --> DB
    API --> W
    API --> M

    %% Testing & Deployment
    Tests[Testing\nUnit + Integration]
    Deploy[Deployment & Responsiveness\nGitHub Pages + iPad/4K UI]

    W --> Tests
    V --> Tests
    M --> Tests
    API --> Tests

    Tests --> Deploy
    W --> Deploy
    M --> Deploy
```

---

## 🚀 AssistantManager Progress Tracker

```mermaid
gantt
    title AssistantManager Project Roadmap
    dateFormat  YYYY-MM-DD
    axisFormat  %b %Y

    section Foundation
    Setup Solution & Projects        :done,    des1, 2025-11-01, 5d
    Configure .NET 10 + GitHub Pages :active,  des2, 2025-11-06, 5d
    Setup SQLite + EF Core           :         des3, 2025-11-11, 5d

    section Core Models
    Player & Event Models            :         des4, 2025-11-16, 7d
    Position/Injury/Rating Models    :         des5, 2025-11-23, 5d
    SuspensionRecord                 :         des6, 2025-11-28, 3d

    section Suspension Logic
    Implement Rules & Workflow       :         des7, 2025-12-01, 7d

    section ViewModels
    SquadSelectionViewModel          :         des8, 2025-12-08, 5d
    EventDetailViewModel             :         des9, 2025-12-13, 5d

    section UI Pages
    SquadSelection.razor             :         des10, 2025-12-18, 5d
    EventDetail.razor                :         des11, 2025-12-23, 5d
    Matchday.razor                   :         des12, 2025-12-28, 5d

    section Components
    FieldZone.razor                  :         des13, 2026-01-02, 3d
    BenchList.razor                  :         des14, 2026-01-05, 3d
    PlayerCard.razor                 :         des15, 2026-01-08, 3d

    section Training Module (Unity)
    DrillLibrary.cs                  :         des16, 2026-01-11, 5d
    TrainingPlanner.razor            :         des17, 2026-01-16, 5d
    DrillViewer.razor                :         des18, 2026-01-21, 5d

    section API Controllers
    PlayersController                :         des19, 2026-01-26, 3d
    EventsController                 :         des20, 2026-01-29, 3d
    AvailabilityController           :         des21, 2026-02-01, 3d
    SquadController                  :         des22, 2026-02-04, 3d

    section Testing
    SquadBuilderService Tests        :         des23, 2026-02-07, 5d
    ViewModel Tests                  :         des24, 2026-02-12, 5d
    Drill Export/Playback Tests      :         des25, 2026-02-17, 5d

    section Deployment & Responsiveness
    Configure GitHub Pages           :         des26, 2026-02-22, 5d
    Offline Support (manifest/sw)    :         des27, 2026-02-27, 3d
    Responsive Design (iPad/4K)      :         des28, 2026-03-02, 5d
```

### 🎯 Milestone 1 – Foundation

- [x] Create VS2026 solution `AssistantManager`
- [x] Add projects: Client (Blazor WASM), Server (ASP.NET Core Web API), Shared (class library)
- [ ] Configure for .NET 10 and GitHub Pages deployment
- [ ] Set up SQLite + EF Core

---

### 🎯 Milestone 2 – Core Domain Models

- [ ] Implement `Player` model (details, preferences, injuries, availability, ratings)
- [ ] Implement `Event` model (date, type, location, notes, availability)
- [ ] Implement `PositionPreference`, `InjuryRecord`, `CoachRating`
- [ ] Implement `SuspensionRecord`

---

### 🎯 Milestone 3 – Suspension Logic

- [ ] Build suspension rules (IFAB + NSFA compliance)
- [ ] Handle red cards, second yellows, accumulated yellows (5, 8, 11)
- [ ] Reset yellow count after season
- [ ] Carry over unserved suspensions
- [ ] Exclude suspended players unless `CoachOverride == true`

---

### 🎯 Milestone 4 – Client ViewModels

- [ ] Create `SquadSelectionViewModel` (drag/drop, auto-suggest)
- [ ] Create `EventDetailViewModel` (load, update, save availability)

---

### 🎯 Milestone 5 – UI Pages

- [ ] `SquadSelection.razor` (drag/drop squad builder)
- [ ] `EventDetail.razor` (event form + availability)
- [ ] `Matchday.razor` (squad, formation, subs, stats, suspensions — no drills)

---

### 🎯 Milestone 6 – Components

- [ ] `FieldZone.razor` (drop target, lockable)
- [ ] `BenchList.razor` (draggable unassigned players)
- [ ] `PlayerCard.razor` (compact info, suspension indicators)

---

### 🎯 Milestone 7 – Training Module (Unity Integration)

- [ ] `DrillLibrary.cs` (manage drill metadata, link Unity exports)
- [ ] `TrainingPlanner.razor` (combine drills into sessions, attendance, printable sheets)
- [ ] `DrillViewer.razor` (Three.js/Babylon.js playback of Unity JSON drills)

---

### 🎯 Milestone 8 – API Controllers

- [ ] `PlayersController` (CRUD)
- [ ] `EventsController` (CRUD)
- [ ] `AvailabilityController` (get/set availability)
- [ ] `SquadController` (suggest squad)

---

### 🎯 Milestone 9 – Testing

- [ ] Unit tests for `SquadBuilderService`
- [ ] Unit tests for `SquadSelectionViewModel` + `EventDetailViewModel`
- [ ] Tests for Unity drill export → JSON → playback pipeline

---

### 🎯 Milestone 10 – Deployment & Responsiveness

- [ ] Configure Blazor WASM for GitHub Pages deployment
- [ ] Add manifest.json + service worker for offline support
- [ ] Optimize UI for iPad Air (5th gen) and 4K desktop screens

---
