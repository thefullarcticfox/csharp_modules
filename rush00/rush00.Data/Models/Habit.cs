using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rush00.Data.Models
{
    public class Habit
    {
        [Key, Required]
        public int HabitId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Motivation { get; set; }
        [Required]
        public bool IsFinished { get; set; }

        public List<HabitCheck> HabitChecks { get; } = new List<HabitCheck>();
    }
}
