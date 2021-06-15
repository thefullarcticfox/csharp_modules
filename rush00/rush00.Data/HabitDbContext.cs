using Microsoft.EntityFrameworkCore;
using rush00.Data.Models;

namespace rush00.Data
{
    public class HabitDbContext : DbContext
    {
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCheck> HabitChecks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("Filename=habits.db");
    }
}
