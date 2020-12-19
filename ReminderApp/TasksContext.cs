using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;

namespace ReminderApp
{
    public partial class TasksContext : DbContext
    {
        public TasksContext() : base()
        {
        }

        public TasksContext(DbContextOptions<TasksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompletedTask> CompletedTasks { get; set; }
        public virtual DbSet<CurrentTask> CurrentTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder.UseMySQL(System.Configuration.ConfigurationManager.
                    ConnectionStrings["TasksConnectionString"].ConnectionString);
            }
        }
    }
}
