using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using rush00.App.ViewModels;
using rush00.Data;
using rush00.Data.Models;

namespace rush00.App.Views
{
    public class HabitTrackerView : UserControl
    {
        public HabitTrackerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ToggleButton_OnChecked(object? sender, RoutedEventArgs e)
        {
            var ck = sender as CheckBox;
            if (ck?.DataContext is not HabitCheck dataCtx)
                return;

            using var dbCtx = new HabitDbContext();

            Habit? habit = dbCtx.Habits
                .Include(x => x.HabitChecks)
                .FirstOrDefault(x => dataCtx.HabitId == x.Id);

            HabitCheck? habitCheck = habit?.HabitChecks.FirstOrDefault(x => dataCtx.Id == x.Id);

            if (habitCheck == null || habit == null)
                return;

            habitCheck.IsChecked = true;
            dbCtx.SaveChanges();

            HabitCheck? lastCheck = habit.HabitChecks.OrderBy(x => x.Date).Last();
            if (lastCheck.IsChecked || lastCheck.Date < DateTimeOffset.Now)
            {
                habitCheck.Habit.IsFinished = true;
                dbCtx.SaveChanges();
                var mainWindowVm = Parent?.DataContext as MainWindowViewModel;
                mainWindowVm?.CongratsOnHabit();
            }

            e.Handled = true;
        }

        private void ToggleButton_OnUnchecked(object? sender, RoutedEventArgs e)
        {
            var ck = sender as CheckBox;
            if (ck?.DataContext is not HabitCheck dataCtx)
                return;
            ck.IsChecked = dataCtx.IsChecked;
            e.Handled = true;
        }
    }
}
