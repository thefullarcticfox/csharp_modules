using System.Collections.Generic;
using System.Collections.ObjectModel;
using rush00.Data.Models;

namespace rush00.App.ViewModels
{
    public class HabitTrackerViewModel : ViewModelBase
    {
        public HabitTrackerViewModel(IEnumerable<HabitCheck> habitChecks)
        {
            HabitChecks = new ObservableCollection<HabitCheck>(habitChecks);
        }

        public ObservableCollection<HabitCheck> HabitChecks { get; set; }
    }
}
