#nullable enable
using System;
using System.Collections.Generic;
using d01_ex01.Events;

namespace d01_ex01.Tasks
{
    public class Task
    {
        public string Title { get; }
        public TaskType Type { get; }
        public TaskPriority Priority { get; private set; }
        public DateTime? DueDate { get; private set; }
        public string? Summary { get; private set; }
        public List<Event> History { get; }

        public Task(string title, TaskType type, TaskPriority priority = TaskPriority.Normal,
            DateTime? dueDate = null, string? summary = null)
        {
            Title = title;
            Summary = summary;
            DueDate = dueDate;
            Priority = priority;
            Type = type;
            History = new List<Event> { new CreatedEvent() };
        }

        private TaskState GetState() => History[^1].State;

        public void SetDone()
        {
            if (GetState() != TaskState.Done)
                History.Add(new TaskDoneEvent());
            Console.WriteLine($"Task [{Title}] is done!");
        }

        public void SetWontDo()
        {
            if (GetState() == TaskState.Done)
            {
                Console.WriteLine($"Error: Task [{Title}] is done.");
                return;
            }
            History.Add(new TaskWontDoEvent());
            Console.WriteLine($"Task [{Title}] is no longer relevant!");
        }

        public override string ToString()
        {
            string res = $"- {Title}\n" +
                         $"[{Type.ToString()}] [{GetState().ToString()}]\n" +
                         $"Priority: {Priority.ToString()}, Due till {DueDate:d}";
            if (!string.IsNullOrEmpty(Summary))
                res += $"\n{Summary}";
            return res;
        }

        public static Task CreateTask()
        {
            Console.WriteLine("Enter title");
            string? title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Input error. Check input data and try again.");

            Console.WriteLine("Enter summary");
            string? summary = Console.ReadLine();

            Console.WriteLine("Enter dueDate");
            string? dueDateStr = Console.ReadLine();

            Console.WriteLine("Enter type [Work, Study, Personal]");
            string? typeStr = Console.ReadLine();
            if (!Enum.TryParse(typeStr, true, out TaskType type))
                throw new ArgumentException("Input error. Check input data and try again.");

            // creating task
            var result = new Task(title, type);

            // optional parameters
            Console.WriteLine("Enter priority [Low, Normal, High]");
            string? priorityStr = Console.ReadLine();
            if (Enum.TryParse(priorityStr, true, out TaskPriority priority))
                result.Priority = priority;
            if (DateTime.TryParse(dueDateStr, out DateTime dueDate))
                result.DueDate = dueDate;
            if (!string.IsNullOrEmpty(summary))
                result.Summary = summary;

            return result;
        }
    };
}
