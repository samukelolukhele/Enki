using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Model;

namespace server.Data
{
    public class ServerDBContext : DbContext
    {
        public ServerDBContext(DbContextOptions<ServerDBContext> options)
        : base(options)
        { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<DayPlan> DayPlans { get; set; } = null!;
        public DbSet<Model.Task> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany<DayPlan>(u => u.day_plans)
            .WithOne(dp => dp.user)
            .HasForeignKey(dp => dp.user_id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DayPlan>()
            .HasMany(dp => dp.tasks)
            .WithOne(t => t.day_plan)
            .HasForeignKey(t => t.day_plan_id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Model.Task>()
            .HasMany(t => t.milestones)
            .WithOne(m => m.task)
            .HasForeignKey(m => m.task_id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
