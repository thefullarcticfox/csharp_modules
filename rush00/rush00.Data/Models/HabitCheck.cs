using System;
using System.ComponentModel.DataAnnotations;

namespace rush00.Data.Models
{
    public class HabitCheck
    {
        [Key, Required]
        public int HabitCheckId { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        public bool IsChecked { get; set; }

        public int HabitId { get; set; }
        public Habit Habit { get; set; }
    }
}
