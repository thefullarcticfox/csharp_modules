using System;
using System.Collections.Generic;
using System.Globalization;
using d01_ex01.Tasks;

CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

var tracker = new TaskTracker();
while (true)
{
    Console.Write("Enter your command [add, list, done, wontdo, quit]: ");
    string cmd = Console.ReadLine();
    if (cmd == "add")
        tracker.AddTask();
    else if (cmd == "list")
        tracker.ListTasks();
    else if (cmd is "done" or "wontdo")
        tracker.SetTaskState(cmd);
    else if (cmd is "q" or "quit")
        break;
}

internal class TaskTracker
{
    private readonly List<Task> _tasks;

    public TaskTracker() => _tasks = new List<Task>();

    public void AddTask()
    {
        try
        {
            _tasks.Add(Task.CreateTask());
            Console.WriteLine();
            Console.WriteLine($"- {_tasks[^1]}");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine();
    }

    public void ListTasks()
    {
        if (_tasks.Count == 0)
            Console.WriteLine("Task list is empty.");
        else
        {
            foreach (Task task in _tasks)
                Console.WriteLine($"- {task}" + Environment.NewLine);
        }
    }

    public void SetTaskState(string state)
    {
        Console.WriteLine("Enter title");
        string title = Console.ReadLine();
        Task todo = _tasks.Find(task => task.Title == title);
        if (todo == null)
            Console.WriteLine("Input error. No such task.");
        else
        {
            if (state == "done")
                todo.SetDone();
            else
                todo.SetWontDo();
        }

        Console.WriteLine();
    }
}
