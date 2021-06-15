using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using rush00.Data;
using System.Linq;
using System.Reactive.Linq;

namespace rush00.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase? _content;
        private HabitDbContext? _dbContext;

        public string Title { get; private set; }

        public ViewModelBase? Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public MainWindowViewModel()
        {
            Title = "";
            _dbContext = new HabitDbContext();
            var habit = _dbContext.Habits
                .Include(x => x.HabitChecks)
                .FirstOrDefault(x => !x.IsFinished);

            if (habit == null)
                CreateHabit();
        }

        public void CreateHabit()
        {
            Title = "Set new habit to track";
            var vm = new HabitCreateViewModel();
            Content = vm;
/*            vm.BeginHabit
                .Subscribe(model =>
                {
                    _db.Habits.Add(model);

                    var dates = new DateTimeOffset(vm.StartDate);
                    for (int i = 0; i < vm.DaysCount; i++)
                        _db.HabitChecks.Add(new Data.Models.HabitCheck {  });
                });*/
        }
    }
}
