using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CookingPlan.Models;

namespace CookingPlan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CookingPlan.Models.Plan> Plan { get; set; }
        public DbSet<CookingPlan.Models.Meal> Meal { get; set; }
        public DbSet<CookingPlan.Models.Ingredient> Ingredient { get; set; }
        public DbSet<CookingPlan.Models.Food> Food { get; set; }
    }
}
