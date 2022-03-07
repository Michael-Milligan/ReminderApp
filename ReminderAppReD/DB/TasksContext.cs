using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ReminderAppReD.DB
{
    public partial class TasksContext : DbContext
    {
        public TasksContext()
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
                optionsBuilder.UseSqlite(ReminderAppReD.Properties.Resources.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompletedTask>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.completionDateTime)
                    .IsRequired()
                    .HasColumnType("DATETIME");

                entity.Property(e => e.taskId)
                .IsRequired()
                .HasColumnName("TaskID");

                entity.HasOne(d => d.task)
                    .WithMany(p => p.completedTasks)
                    .HasForeignKey(d => d.taskId);
            });

            modelBuilder.Entity<CurrentTask>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("ID");

                entity.Property(e => e.dateTime)
                    .IsRequired()
                    .HasColumnType("String")
                    .HasColumnName("DateTime");

                entity.Property(e => e.task)
                    .IsRequired()
                    .HasColumnType("String");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
