# Mesen .NET Frontend

An experimental .NET 10 WPF frontend for the Mesen emulator core.

The goal of this project is to explore native interoperability between .NET and Mesen while building a custom desktop frontend with support for localization, video rendering, input, audio, and emulator controls.

> This is an unofficial project and is not affiliated with or endorsed by the Mesen project.

## Current status

The project can currently:

- Load `MesenCore.dll`
- Initialize the Mesen emulator core
- Open NES ROM files
- Start ROM execution
- Check the emulator running state

Video, audio, and input integration are still under development.

## Tech stack

- .NET 10
- C#
- WPF
- Native interoperability / P/Invoke
- Mesen emulator core

## Project structure

```text
mesen-dotnet-frontend/
├── mesen-dotnet-frontend.sln
├── MesenDotNetFrontend.Core/
│   ├── Emulation/
│   ├── Interop/
│   └── Models/
│
└── MesenDotNetFrontend.Wpf/
    ├── Native/
    ├── Resources/
    ├── App.xaml
    └── MainWindow.xaml