using System;

namespace d01_ex01.Tasks
{
    public record Task
    {
        public string           Title { get; init; }
        public string           Description { get; init; }
        public DateTime         Deadline { get; init; }
        public TaskPriority     Priority { get; init; }
        public TaskState        State { get; init; }
        public TaskType         Type { get; init; }
    };
}
