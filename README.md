# SimpleKanban

A minimal WPF Kanban-style task dashboard built with .NET 8. This project demonstrates basic WPF concepts including MVVM pattern, data binding, and drag-and-drop functionality.

## Project Status
**In Progress** - This is a learning project where I'm exploring WPF development, MVVM architecture, and C# best practices. Features are being added incrementally.

## Features
- Kanban board with customizable categories (Backlog, In Progress, Bugs, Fixed)
- Drag and drop items between categories
- Add/edit/delete items with title and description
- Customizable Tag system for categorizing items
- Item count display for each category
- Settings window to edit categories and tags
- Persistence through json file save/load

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

## Screenshots

<p align="center">
  <img src="https://github.com/user-attachments/assets/9d8c3f50-fcc4-4556-88b4-861200953811" alt="Main Kanban Board" width="800" style="border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1);">
  <br><br>
  <img src="https://github.com/user-attachments/assets/f9bfa545-a93b-41cc-b6e5-5777c0a35a29" alt="Task Management View" width="800" style="border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1);">
  <br><br>
  <img src="https://github.com/user-attachments/assets/daaea82c-057a-47aa-86c5-c00cc5108336" alt="Add New Task" width="500" style="border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1);">
</p>
