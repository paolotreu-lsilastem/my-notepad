# MyNotepad

A classic Notepad-like application built with C# Windows Forms.

## Features
- Single document per instance
- New, Open, Save, Save As
- Default font: Consolas (monospace)
- Command-line file support: open file if one, launch N instances if multiple
- UI in English only

## Usage
- Run the application normally for a blank document.
- Pass a filename as a command-line argument to open it.
- Pass multiple filenames to open multiple instances (one per file).

## Build
- Requires .NET 6.0 or later.
- To build: `dotnet build`
- To run: `dotnet run -- [filename1] [filename2] ...`
