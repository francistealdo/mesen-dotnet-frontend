# Mesen .NET Frontend

An experimental .NET 10 WPF frontend for the Mesen emulator core.

The goal of this project is to explore native interoperability between .NET and Mesen while building a custom desktop frontend with support for localization, video rendering, input, audio, and emulator controls.

> This is an unofficial project and is not affiliated with or endorsed by the Mesen project.

## Current status

The project can currently:

- Load and initialize `MesenCore.dll`
- Open and run NES ROM files
- Apply a basic NES core configuration
- Receive software-rendered video frames from Mesen
- Display NES video output in a WPF `WriteableBitmap`
- Resize the renderer with the application window

Audio and input integration are still under development.

## Tech stack

- .NET 10
- C#
- WPF
- Native interoperability / P/Invoke
- Mesen emulator core

## Project structure

```text
mesen-dotnet-frontend/
├── mesen-dotnet-frontend.slnx
├── MesenDotNetFrontend.Core/
│   ├── Emulation/
│   └── Interop/
└── MesenDotNetFrontend.Wpf/
    ├── Native/
    ├── Resources/
    ├── App.xaml
    └── MainWindow.xaml
```

## MesenCore.dll

This repository does not include Mesen binaries.

Download/build a compatible MesenCore.dll and place it at:

MesenDotNetFrontend.Wpf/Native/MesenCore.dll

## License

This project is licensed under the GNU General Public License v3.0.

It uses the Mesen emulator core, which is also licensed under the GNU GPL v3.
Mesen is a separate project and is not affiliated with or endorsed by this project.