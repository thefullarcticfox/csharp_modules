using ReactiveUI;
using rush00.Data.Models;
using System.Linq;
using System.Reactive;

namespace rush00.App.ViewModels
{
    public class HabitFinishViewModel : ViewModelBase
    {
        private Habit _habit;

        public HabitFinishViewModel(Habit habit)
        {
            _habit = habit;
            CreateNew = ReactiveCommand.Create(() => Unit.Default);
        }

        public int DaysChecked => _habit.HabitChecks.Where(x => x.IsChecked).Count();

        public int DaysTotal => _habit.HabitChecks.Count();

        public string ResultString => $"{DaysChecked}/{DaysTotal} days checked";

        public string Motivation => $"Finally: {_habit.Motivation}";

        public ReactiveCommand<Unit, Unit> CreateNew { get; }
    }
}
