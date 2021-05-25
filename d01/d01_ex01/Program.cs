#nullable enable
using System;
using System.Collections.Generic;
using d01_ex01.Tasks;

var tasks = new List<Task>();

void AddTask()
{
    try
    {
        tasks.Add(Task.CreateTask());
        Console.WriteLine(tasks[^1].ToString());
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
    }
}

void ListTasks()
{
    if (tasks.Count == 0)
        Console.WriteLine("Task list is empty.");
    else
        foreach (Task task in tasks)
            Console.WriteLine(task.ToString());
}

void SetTaskState(string state)
{
    Console.WriteLine("Enter title");
    string? title = Console.ReadLine();
    Task? todo = tasks.Find(task => task.Title == title);
    if (todo == null)
        Console.WriteLine("Input error. No such task.");
    else
    {
        if (state == "done")
            todo.SetDone();
        else
            todo.SetWontDo();
    }
}

while (true)
{
    Console.Write("\nEnter your command [add, list, done, wontdo, quit]: ");
    string? cmd = Console.ReadLine();
    if (cmd == "add")
        AddTask();
    else if (cmd == "list")
        ListTasks();
    else if (cmd is "done" or "wontdo")
        SetTaskState(cmd);
    else if (cmd is "q" or "quit")
        break;
}
