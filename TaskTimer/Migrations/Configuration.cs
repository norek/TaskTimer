using TaskTimer.Models;

namespace TaskTimer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskTimer.Models.TaskTimerDbContext>
    {
        public Configuration()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TaskTimerDbContext>());
            AutomaticMigrationsEnabled = false;
            ContextKey = "TaskTimer.Models.TaskTimerDbContext";
        }

        protected override void Seed(TaskTimer.Models.TaskTimerDbContext context)
        {
            context.Projects.AddOrUpdate(p => p.Id,new Project[] {new Project() {Id = 1,Name = "POS"}, new Project() { Id = 2, Name = "eMailer" } , new Project() { Id = 3, Name = "Repricer" } });
        }
    }
}
