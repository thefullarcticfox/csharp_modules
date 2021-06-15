using ReactiveUI;
using rush00.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;

namespace rush00.App.ViewModels
{
    public class HabitTrackerViewModel : ViewModelBase
    {
        public HabitTrackerViewModel(IEnumerable<HabitCheck> habitChecks)
        {
            HabitChecks = new ObservableCollection<HabitCheck>(habitChecks);
            HabitChecks.CollectionChanged += HabitChecks_CollectionChanged;
        }

        private void HabitChecks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<HabitCheck> HabitChecks { get; }
    }
}
