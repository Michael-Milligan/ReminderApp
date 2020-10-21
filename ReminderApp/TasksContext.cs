﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReminderApp
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Tasks;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompletedTask>(entity =>
            {
                entity.HasKey(e => e.CompletionDateTime)
                    .HasName("PK_CDT");

                entity.Property(e => e.CompletionDateTime).HasColumnType("datetime");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CompletedTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TID");
            });

            modelBuilder.Entity<CurrentTask>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Task).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
