# Mesen .NET Frontend

A custom .NET 10 WPF frontend for the Mesen emulator core.

The project explores native interoperability between .NET and Mesen while providing a custom Windows desktop interface for running NES games, managing controls, save states, and emulator features.

> This is an unofficial project and is not affiliated with or endorsed by the Mesen project.

## Current status

The application currently supports:

- Loading and initializing `MesenCore.dll`
- Opening and running NES ROM files
- Software-rendered video output through WPF
- Native audio through Mesen
- Dynamic renderer resizing
- Keyboard input
- Configurable NES controls
- Persistent control settings
- Pause and resume
- Reset
- Close ROM
- Hold-to-fast-forward
- Configurable fast-forward key
- Quick Save and Quick Load
- Named save states
- Native Mesen save-state previews
- Save-state persistence using SQLite
- Save-state browser with preview, date, name, and ROM information
- Save-state deletion
- Application event logging
- No-ROM splash screen
- Application icon and branding
- About window with project information

## Save states

Save states are stored in the application's SQLite database.

Each save can contain:

- ROM name
- ROM hash support
- Save name
- Mesen state data
- Native Mesen screenshot preview
- Creation date

Save-state files are generated using Mesen's native `.mss` format before being stored as binary data in SQLite.

The application also supports:

| Shortcut | Action |
| --- | --- |
| `F1` | Quick Save |
| `F2` | New Save |
| `F3` | Quick Load |
| `F4` | Load Game |

### New Save

`New Save...` opens a dialog containing:

- The current game screenshot
- A custom save name
- Save and Cancel actions

### Load Game

`Load Game...` displays saved states for the currently loaded ROM.

The list includes:

- Screenshot
- Date
- Save name
- ROM name

A save can be selected and loaded or deleted.

## Controls

NES keyboard controls can be configured from:

```text
Settings > Controls...
```

Control mappings are persisted in SQLite and can be changed while a game is running.

The fast-forward key is also configurable.

## Persistence

Application data is stored in SQLite using `Microsoft.Data.Sqlite`.

The database is automatically created under the current user's local application data directory:

```text
%LocalAppData%\MesenDotNetFrontend\mesen-dotnet-frontend.db
```

The database currently contains data for:

- Application logs
- Input settings
- Save states

## Video

Video frames are received directly from the Mesen software renderer.

The native framebuffer is copied into a WPF `WriteableBitmap` and displayed using nearest-neighbor scaling to preserve the original pixel-art appearance.

## Audio

Audio is handled through Mesen's native audio system.

The frontend initializes the Mesen audio configuration and allows the native core to provide audio output directly on Windows.

## Tech stack

- .NET 10
- C#
- WPF
- SQLite
- Microsoft.Data.Sqlite
- Native interoperability / P/Invoke
- Mesen emulator core

## Project structure

```text
mesen-dotnet-frontend/
├── mesen-dotnet-frontend.slnx
├── LICENSE
├── README.md
│
├── MesenDotNetFrontend.Core/
│   ├── Emulation/
│   ├── Interop/
│   └── Models/
│
├── MesenDotNetFrontend.Infrastructure/
│   └── Persistence/
│
└── MesenDotNetFrontend.Wpf/
    ├── About/
    ├── Input/
    ├── Native/
    ├── Resources/
    ├── SaveStates/
    ├── Settings/
    ├── App.xaml
    └── MainWindow.xaml
```

### Core

`MesenDotNetFrontend.Core` contains the emulator-facing logic and native interoperability layer.

Responsibilities include:

- Mesen initialization
- ROM loading
- Video callbacks
- Audio configuration
- NES configuration
- Emulator controls
- Save-state operations
- Native API declarations

### Infrastructure

`MesenDotNetFrontend.Infrastructure` contains persistence-related functionality.

Responsibilities include:

- SQLite database initialization
- Application logging
- Input settings persistence
- Save-state persistence

### WPF

`MesenDotNetFrontend.Wpf` contains the desktop user interface.

Responsibilities include:

- Main emulator window
- Video presentation
- Keyboard input
- Control configuration
- Save-state dialogs
- Load-game browser
- Splash screen
- About window

## MesenCore.dll

This repository does not include Mesen binaries.

Download or build a compatible `MesenCore.dll` and place it at:

```text
MesenDotNetFrontend.Wpf/Native/MesenCore.dll
```

The project currently targets:

```text
MesenCE 2.2.1
```

The DLL is copied to the application output directory during build.

## ROM files

ROM files are not included with this project.

Users are responsible for providing their own legally obtained ROM files.

## Building

Requirements:

- Windows
- Visual Studio with .NET desktop development support
- .NET 10 SDK
- Compatible `MesenCore.dll`

Clone the repository:

```bash
git clone https://github.com/francistealdo/mesen-dotnet-frontend.git
```

Open:

```text
mesen-dotnet-frontend.slnx
```

Place `MesenCore.dll` in:

```text
MesenDotNetFrontend.Wpf/Native/
```

Then build and run `MesenDotNetFrontend.Wpf`.

## Roadmap

Possible future improvements include:

- ROM hash-based save-state association
- Recent games
- Game library
- Play-time tracking
- Improved save-state management
- Additional emulator settings
- Renderer performance improvements
- Additional keyboard shortcuts
- Localization
- Packaging and distribution

## Author

Developed by **Francis Tealdo**  
Software Engineer

Project repository:

https://github.com/francistealdo/mesen-dotnet-frontend

## License

This project is licensed under the GNU General Public License v3.0.

It uses the Mesen emulator core, which is also licensed under the GNU GPL v3.

Mesen is a separate project and is not affiliated with or endorsed by this project.