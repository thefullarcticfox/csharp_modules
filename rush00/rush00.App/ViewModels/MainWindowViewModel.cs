using System;
using System.Linq;
using System.Reactive.Linq;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using rush00.Data;
using rush00.Data.Models;

namespace rush00.App.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title;
        private ViewModelBase? _content;

        public string Title
        {
            get => _title;
            private set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public ViewModelBase? Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public MainWindowViewModel()
        {
            _title = "";
            TrackHabit();
        }

        public void TrackHabit()
        {
            using var dbCtx = new HabitDbContext();

            Habit? currentHabit = dbCtx.Habits
                .Include(x => x.HabitChecks)
                .FirstOrDefault(x => !x.IsFinished);

            if (currentHabit == null)
            {
                CreateHabit();
                return;
            }

            Title = currentHabit.Title;
            Content = new HabitTrackerViewModel(currentHabit.HabitChecks.OrderBy(x => x.Date));
        }

        public void CreateHabit()
        {
            Title = "Set new habit to track";
            var vm = new HabitCreateViewModel();
            vm.BeginHabit.Take(1).Subscribe(model =>
            {
                using (var dbCtx = new HabitDbContext())
                {
                    dbCtx.Habits.Add(model);
                    dbCtx.HabitChecks.AddRange(
                        Enumerable.Range(0, vm.DaysCount)
                        .Select(offset => new HabitCheck
                        {
                            Date = vm.StartDate.Date.AddDays(offset),
                            Habit = model,
                            IsChecked = false
                        })
                        .ToList());
                    dbCtx.SaveChanges();
                }
                TrackHabit();
            });
            Content = vm;
        }

        public void CongratsOnHabit()
        {
            using var dbCtx = new HabitDbContext();

            Habit? finishedHabit = dbCtx.Habits
                .Include(x => x.HabitChecks)
                .OrderBy(x => x.Id)
                .LastOrDefault(x => x.IsFinished);

            if (finishedHabit == null)
            {
                TrackHabit();
                return;
            }

            Title = finishedHabit.Title;
            var vm = new HabitFinishViewModel(finishedHabit);
            vm.CreateNew.Take(1).Subscribe(_ => CreateHabit());
            Content = vm;
        }
    }
}
