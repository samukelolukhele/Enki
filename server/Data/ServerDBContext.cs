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
        public DbSet<Milestone> Milestones { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().Navigation(u => u.day_plans).AutoInclude();
            modelBuilder.Entity<DayPlan>().Navigation(dp => dp.tasks).AutoInclude();


            base.OnModelCreating(modelBuilder);
        }

    }

}
