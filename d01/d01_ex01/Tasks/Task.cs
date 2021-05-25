using System;

namespace d01_ex01.Tasks
{
    public struct Task
    {
        public string Title;
        public string Description;
        public DateTime Deadline;
        public TaskPriority Priority;
        public TaskState State;
        public TaskType Type;
    };
}
