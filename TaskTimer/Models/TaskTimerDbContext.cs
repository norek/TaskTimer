using System.Data.Entity;

namespace TaskTimer.Models
{
    class TaskTimerDbContext : DbContext
    {
        public TaskTimerDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskTimerDbContext>());
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}