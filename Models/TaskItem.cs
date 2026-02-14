csharp Models/TaskItem.cs
using System;

namespace SimpleKanban;
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string Category { get; set; } = "Category";
}