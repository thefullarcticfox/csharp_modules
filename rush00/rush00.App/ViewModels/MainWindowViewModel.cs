using System;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using rush00.Data;

namespace rush00.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase? _content;
        private readonly HabitDbContext? _dbContext;
        private Data.Models.Habit? _currentHabit;

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
            _currentHabit = _dbContext.Habits
                .Include(x => x.HabitChecks)
                .FirstOrDefault(x => !x.IsFinished);

            if (_currentHabit == null)
                CreateHabit();
            else
                TrackHabit();
        }

        public void TrackHabit()
        {
            if (_currentHabit == null)
            {
                _currentHabit = _dbContext?.Habits
                    .Include(x => x.HabitChecks)
                    .FirstOrDefault(x => !x.IsFinished);
            }
            if (_currentHabit != null)
            {
                Title = _currentHabit.Title;
                var vm = new HabitTrackerViewModel(_currentHabit.HabitChecks);
                Content = vm;
            }
        }

        public void CreateHabit()
        {
            Title = "Set new habit to track";
            var vm = new HabitCreateViewModel();
            Content = vm;
            vm.BeginHabit.Take(1).Subscribe(model =>
            {
                if (_dbContext != null)
                {
                    _dbContext.Habits.Add(model);
                    _dbContext.HabitChecks.AddRange(
                        Enumerable.Range(0, vm.DaysCount)
                        .Select(offset => new Data.Models.HabitCheck {
                            Date = vm.StartDate.Date.AddDays(offset),
                            Habit = model,
                            IsChecked = false})
                        .ToList());
                    _dbContext.SaveChanges();
                    TrackHabit();
                }
            });
        }
    }
}
