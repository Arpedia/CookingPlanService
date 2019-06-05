using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CookingPlan.Data
{
    public partial class CookingPlanDbContext : DbContext
    {
        public CookingPlanDbContext()
        {
        }

        public CookingPlanDbContext(DbContextOptions<CookingPlanDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LUNASKY-DESK\\SQLEXPRESS;Database=CookingPlanDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
