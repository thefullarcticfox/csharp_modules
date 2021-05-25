using System;
using d01_ex01.Tasks;

namespace d01_ex01.Events
{
    public record TaskDoneEvent() : Event(TaskState.Done);
}
