using System;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using rush00.Data.Models;

namespace rush00.App.ViewModels
{
    public class HabitCreateViewModel : ViewModelBase
    {
        private string _title;
        private string _motivation;
        private DateTimeOffset _startDate;
        private int _daysCount;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string Motivation
        {
            get => _motivation;
            set => this.RaiseAndSetIfChanged(ref _motivation, value);
        }

        public DateTimeOffset StartDate
        {
            get => _startDate;
            set => this.RaiseAndSetIfChanged(ref _startDate, value);
        }

        public int DaysCount
        {
            get => _daysCount;
            set => this.RaiseAndSetIfChanged(ref _daysCount, value);
        }

        public HabitCreateViewModel()
        {
            _title = "";
            _motivation = "";
            _startDate = DateTimeOffset.Now;
            _daysCount = 0;

            var startEnabled = this.WhenAnyValue(
                x => x.Title, x => x.Motivation, x => x.DaysCount,
                (x, y, z) =>
                !string.IsNullOrWhiteSpace(x) &&
                !string.IsNullOrWhiteSpace(y) &&
                z > 0);

            BeginHabit = ReactiveCommand.Create(
                () => new Habit
                {
                    Title = Title,
                    Motivation = Motivation,
                    IsFinished = false
                },
                startEnabled);
        }

        public ReactiveCommand<Unit, Habit> BeginHabit { get; }
    }
}
