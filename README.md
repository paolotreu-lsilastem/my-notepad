# MyNotepad

A classic Notepad-like application built with C# Windows Forms.

## Features
- Single document per instance
- New, Open, Save, Save As
- Default font: Consolas (monospace)
- Command-line file support: open file if one, launch N instances if multiple
- UI in English only
- Window opens centered on screen

## Usage
- Run the application normally for a blank document.
- Pass a filename as a command-line argument to open it.
- Pass multiple filenames to open multiple instances (one per file).

## Build
- Requires .NET 9.0 or later.
- To build: `dotnet build`
- To run: `dotnet run -- [filename1] [filename2] ...`

## Version
- 1.0.1: Window opens centered on screen
- 1.0.0: First public release
