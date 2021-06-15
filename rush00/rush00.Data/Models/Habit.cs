using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rush00.Data.Models
{
    public class Habit
    {
        public Habit() => HabitChecks = new HashSet<HabitCheck>();

        [Key, Required, System.ComponentModel.Description("Unique identifier")]
        public long Id { get; set; }

        public string Title { get; set; }
        
        public string Motivation { get; set; }
        
        public bool IsFinished { get; set; }

        public virtual ICollection<HabitCheck> HabitChecks { get; set; }
    }
}
