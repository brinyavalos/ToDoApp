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
        return $"ID: {Id} | Title: {Title} | Priority: {Priority} | Deadline: {Deadline:MM/dd/yyyy} | Completed: {IsCompleted}";
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
            Console.WriteLine("2. Set Due Date");
            Console.WriteLine("3. Edit Task");
            Console.WriteLine("4. Receive Reminders");
            Console.WriteLine("5. Mark Task Complete");
            Console.WriteLine("6. Set Task Priority");
            Console.WriteLine("7. View To-Do List");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    SetDueDate();
                    break;
                case "3":
                    EditTask();
                    break;
                case "4":
                    ReceiveReminders();
                    break;
                case "5":
                    MarkTaskComplete();
                    break;
                case "6":
                    SetTaskPriority();
                    break;
                case "7":
                    ViewTasks();
                    break;
                case "8":
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

        Console.Write("Deadline (MM/DD/YYYY): ");
        DateTime deadline;
        while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out deadline))
        {
            Console.Write("Invalid date format. Try again (MM/DD/YYYY): ");
        }

        Console.Write("Priority (Low, Medium, High): ");
        string priority = GetValidPriority();

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

    private void SetDueDate()
    {
        Console.Clear();
        Console.WriteLine("=== Set Due Date ===");
        ViewTasks();

        Console.Write("Enter Task ID to set due date: ");
        Task task = GetTaskById();

        Console.Write("New Deadline (MM/DD/YYYY): ");
        DateTime newDeadline;
        while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out newDeadline))
        {
            Console.Write("Invalid date format. Try again (MM/DD/YYYY): ");
        }

        task.Deadline = newDeadline;
        Console.WriteLine("Deadline updated successfully! Press Enter to continue.");
        Console.ReadLine();
    }

    private void EditTask()
    {
        Console.Clear();
        Console.WriteLine("=== Edit Task ===");
        ViewTasks();

        Console.Write("Enter Task ID to edit: ");
        Task task = GetTaskById();

        Console.Write("New Title: ");
        task.Title = Console.ReadLine();

        Console.Write("New Description: ");
        task.Description = Console.ReadLine();

        Console.WriteLine("Task updated successfully! Press Enter to continue.");
        Console.ReadLine();
    }

    private void ReceiveReminders()
    {
        Console.Clear();
        Console.WriteLine("=== Upcoming Deadlines ===");

        var upcomingTasks = tasks.Where(t => !t.IsCompleted && t.Deadline <= DateTime.Now.AddDays(1));
        if (!upcomingTasks.Any())
        {
            Console.WriteLine("No tasks with upcoming deadlines.");
        }
        else
        {
            foreach (var task in upcomingTasks)
            {
                Console.WriteLine(task);
            }
        }

        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    private void MarkTaskComplete()
    {
        Console.Clear();
        Console.WriteLine("=== Mark Task as Complete ===");
        ViewTasks();

        Console.Write("Enter Task ID to mark as complete: ");
        Task task = GetTaskById();

        task.IsCompleted = true;
        Console.WriteLine("Task marked as complete! Press Enter to continue.");
        Console.ReadLine();
    }

    private void SetTaskPriority()
    {
        Console.Clear();
        Console.WriteLine("=== Set Task Priority ===");
        ViewTasks();

        Console.Write("Enter Task ID to set priority: ");
        Task task = GetTaskById();

        Console.Write("New Priority (Low, Medium, High): ");
        task.Priority = GetValidPriority();

        Console.WriteLine("Priority updated successfully! Press Enter to continue.");
        Console.ReadLine();
    }

    private void ViewTasks()
    {
        Console.Clear();
        Console.WriteLine("=== View To-Do List ===");

        if (!tasks.Any())
        {
            Console.WriteLine("No tasks available.");
        }
        else
        {
            Console.WriteLine("Current Tasks:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    private Task GetTaskById()
    {
        int taskId;
        while (!int.TryParse(Console.ReadLine(), out taskId) || !tasks.Any(t => t.Id == taskId))
        {
            Console.Write("Invalid ID. Try again: ");
        }

        return tasks.First(t => t.Id == taskId);
    }

    private string GetValidPriority()
    {
        string priority;
        while (true)
        {
            priority = Console.ReadLine();
            if (priority == "Low" || priority == "Medium" || priority == "High")
                break;

            Console.Write("Invalid priority. Enter Low, Medium, or High: ");
        }

        return priority;
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
