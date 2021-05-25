using System;

namespace d01_ex01.Tasks
{
    public struct Task
    {
        private string _title;
        private string _description;
        private DateTime _deadline;
        private TaskPriority _priority;
        private TaskType _type;
        private TaskState _state;

        public Task(string title, string description, DateTime deadline, TaskPriority priority, TaskType type)
        {
            _title = title;
            _description = description;
            _deadline = deadline;
            _priority = priority;
            _type = type;
            _state = TaskState.Created;
        }
    };
}
