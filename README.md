# SimpleKanban

A minimal WPF Kanban-style task dashboard built with .NET 8. This project demonstrates basic WPF concepts including MVVM pattern, data binding, and drag-and-drop functionality.

## Project Status
**In Progress** - This is a learning project where I'm exploring WPF development, MVVM architecture, and C# best practices. Features are being added incrementally. Not able to save any info yet, planning on using SQL for creating databases (NOTE: currently using JSON for info storage: it's the best/easiest for the current scope of the project) and using the kanban interface to interact with it.

## Features
- Kanban board with customizable categories (Backlog, In Progress, Bugs, Fixed)
- Drag and drop items between categories
- Add/edit/delete items with title and description
- Customizable Tag system for categorizing items
- Item count display for each category
- Settings window to edit categories and tags

## Prerequisites
- **.NET 8 SDK** - Download from [Microsoft .NET](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022** or later with WPF/.NET desktop workload
- Windows operating system (WPF requirement)

## Getting Started
1. Clone or download the project
2. Open `SimpleKanban.sln` in Visual Studio
3. Build and run the project (F5 or Ctrl+F5)

## Technologies Used
- C# / .NET 8
- WPF (Windows Presentation Foundation)
- MVVM Pattern (separate UI from logic - data binding)
- Entity Framework Core (planned for future persistence)
- VS Code and Github
