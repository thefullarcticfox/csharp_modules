using System;
using System.ComponentModel.DataAnnotations;

namespace rush00.Data.Models
{
    public class HabitCheck
    {
        [Key, Required, System.ComponentModel.Description("Unique identifier")]
        public long Id { get; set; }
        
        public bool IsChecked { get; set; }
        
        public DateTimeOffset Date { get; set; }

        public virtual Habit Habit { get; set; }
    }
}
