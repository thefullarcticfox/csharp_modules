using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ReactiveUI;
using rush00.Data.Models;

namespace rush00.App.ViewModels
{
    public class HabitTrackerViewModel : ViewModelBase
    {
        public HabitTrackerViewModel(IEnumerable<HabitCheck> habitChecks)
        {
            HabitChecks = new ObservableCollection<HabitCheck>(habitChecks);
            HabitChecks.CollectionChanged += HabitChecks_CollectionChanged;
        }

        private void HabitChecks_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<HabitCheck> HabitChecks { get; }
    }
}
