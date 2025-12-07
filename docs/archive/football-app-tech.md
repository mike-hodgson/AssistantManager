# Football Coaching App - Tech Stack Considerations

## Project Overview

Personal football coaching/management app with two primary use contexts:

- **Desktop (Windows)**: Session planning, drill design, player management
- **Web/iPad**: Match day management, training checklists, portable reference

## Key Features Discussed

### Training Drill Library Module

Need to design 3D training drills showing:

- Part of football field (3D representation)
- Equipment: cones, balls, poles
- Player movements in sequences
- Ability to design/edit at runtime
- Save drills to library for later playback
- Combine drills into training sessions with instructions/notes

### Match Day Features (iPad)

- Substitution management
- Track time spent in each position per player
- Stats tracking: yellow/red cards, goals scored/assisted
- Training session checklist/run list

### Formation Designer

- 2D top-down football field representation
- Drag/drop player icons to set positions
- Save/load team formations

## Recommended Tech Stack

### Desktop Planning Environment

**Primary Authoring:**

- **Unity** (for 3D drill designer/editor)
  - Use Unity Editor as desktop authoring tool
  - Built-in 3D placement, animation paths, timeline sequencing
  - Export drill data (JSON or similar format)
  - WebGL export option available if needed for playback

**Main Application:**

- **Blazor** or **Desktop .NET** (WPF/WinUI)
  - Player data management
  - Drill library metadata
  - Session planning
  - Formation tools
  - Shares C# codebase with web components

### Web/iPad Interface

**Framework:**

- **Blazor WebAssembly**
  - C# codebase shared with desktop components
  - No server required once loaded
  - Works on iPad without Mac/Xcode

**3D Drill Playback:**

- **Three.js** or **Babylon.js** (lightweight viewer)
  - Embedded component for drill visualization
  - Reads drill data exported from Unity
  - Touch-friendly for iPad

**Formation Designer:**

- **Konva.js** or **Fabric.js** (HTML5 Canvas)
  - Purpose-built for interactive 2D graphics
  - Smooth drag-and-drop on touch devices
  - Export/import formation data as JSON

## Architecture Pattern

### Hybrid Workflow

1. **Create/Edit on Desktop**: Use Unity Editor for complex 3D drill design (95% of planning work)
2. **Sync Data**: Export drill definitions to database/cloud storage
3. **Consume on Web**: Lightweight viewer displays drills on iPad when needed
4. **Mobile Focus**: iPad interface prioritized for quick reference, checklists, and real-time match management

### Data Flow

- Unity exports drill sequences → Database
- Desktop app manages player/session data → Database
- Web app reads all data for portable access
- Text instructions prominent in mobile view (3D visualization available on-demand)

## Key Benefits of This Approach

- Unity Editor provides professional 3D authoring tools without building from scratch
- Web app stays lightweight and fast on iPad
- C# codebase reuse across desktop and web (via Blazor)
- No need for Mac/Xcode for iPad deployment
- Canvas libraries handle 2D formation designer efficiently

## Next Steps

- Evaluate Unity for drill editor prototype
- Test Blazor WebAssembly on iPad for performance
- Experiment with Konva.js for formation designer
- Consider Three.js for lightweight drill playback viewer
