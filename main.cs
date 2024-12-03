using System;
using System.Collections.Generic;
using System.Linq;

class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Priority { get; set; } // Low, Medium, High
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        return $"ID: {Id} | Title: {Title} | Priority: {Priority} | Deadline: {Deadline} | Completed: {IsCompleted}";
    }
}

class ToDoListApp
{
    private List<Task> tasks = new List<Task>();
    private int nextId = 1;

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== To-Do List App ===");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Mark Task as Complete");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    MarkTaskComplete();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void AddTask()
    {
        Console.Clear();
        Console.WriteLine("=== Add Task ===");
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Deadline (YYYY-MM-DD): ");
        DateTime deadline;
        while (!DateTime.TryParse(Console.ReadLine(), out deadline))
        {
            Console.Write("Invalid date. Try again (YYYY-MM-DD): ");
        }

        Console.Write("Priority (Low, Medium, High): ");
        string priority;
        while (true)
        {
            priority = Console.ReadLine();
            if (priority == "Low" || priority == "Medium" || priority == "High")
                break;

            Console.Write("Invalid priority. Enter Low, Medium, or High: ");
        }

        tasks.Add(new Task
        {
            Id = nextId++,
            Title = title,
            Description = description,
            Deadline = deadline,
            Priority = priority,
            IsCompleted = false
        });

        Console.WriteLine("Task added successfully! Press Enter to continue.");
        Console.ReadLine();
    }

    private void ViewTasks()
    {
        Console.Clear();
        Console.WriteLine("=== View Tasks ===");

        if (!tasks.Any())
        {
            Console.WriteLine("No tasks available.");
        }
        else
        {
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        Console.WriteLine("Press Enter to go back.");
        Console.ReadLine();
    }

    private void MarkTaskComplete()
    {
        Console.Clear();
        Console.WriteLine("=== Mark Task as Complete ===");

        if (!tasks.Any())
        {
            Console.WriteLine("No tasks available.");
            Console.WriteLine("Press Enter to go back.");
            Console.ReadLine();
            return;
        }

        ViewTasks();
        Console.Write("Enter Task ID to mark as complete: ");
        int taskId;

        while (!int.TryParse(Console.ReadLine(), out taskId) || !tasks.Any(t => t.Id == taskId))
        {
            Console.Write("Invalid ID. Try again: ");
        }

        Task task = tasks.First(t => t.Id == taskId);
        task.IsCompleted = true;

        Console.WriteLine("Task marked as complete! Press Enter to continue.");
        Console.ReadLine();
    }

    private void DeleteTask()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Task ===");

        if (!tasks.Any())
        {
            Console.WriteLine("No tasks available.");
            Console.WriteLine("Press Enter to go back.");
            Console.ReadLine();
            return;
        }

        ViewTasks();
        Console.Write("Enter Task ID to delete: ");
        int taskId;

        while (!int.TryParse(Console.ReadLine(), out taskId) || !tasks.Any(t => t.Id == taskId))
        {
            Console.Write("Invalid ID. Try again: ");
        }

        tasks.RemoveAll(t => t.Id == taskId);

        Console.WriteLine("Task deleted successfully! Press Enter to continue.");
        Console.ReadLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        ToDoListApp app = new ToDoListApp();
        app.Run();
    }
}
