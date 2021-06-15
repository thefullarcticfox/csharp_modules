using rush00.Data.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace rush00.App.ViewModels
{
    public class HabitTrackerViewModel : ViewModelBase
    {
        public HabitTrackerViewModel(IEnumerable<HabitCheck> habitChecks)
        {
            Checks = new ObservableCollection<HabitCheck>(habitChecks);
        }

        public ObservableCollection<HabitCheck> Checks { get; }
    }
}
