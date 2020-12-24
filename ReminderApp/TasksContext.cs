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

        public DbSet<CompletedTask> CompletedTasks { get; set; }
        public DbSet<CurrentTask> CurrentTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"DataSource=Tasks.sqlite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentTask>().HasKey(item => item.Id);
            modelBuilder.Entity<CompletedTask>().HasKey(item => item.Id);
        }
    }
}
