using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingPlan.Data;
using CookingPlan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookingPlan.Controllers
{
    [Authorize]
    public class ItemlistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemlistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var plans = _context.Plan
                .Include(p => p.Meal)
                .Where(p => p.Date >= DateTime.Today && p.Date <= DateTime.Today.AddDays(3));

            foreach (var plan in plans)
            {
                var meal = await _context.Meal.Include(m => m.Ingredients).FirstAsync(m => m.Id == plan.MealId);
                plan.Meal = meal;
            }

            ViewBag.Time = Others.Const.TimeList();
            ViewBag.Date = Others.Const.DateList();
            return View(plans);
        }

        public async Task<IActionResult> Checklist([Bind("Date")] DateTime date)
        {
            ViewBag.selected_date = date;

            var plans = _context.Plan
                .Include(p => p.Meal)
                .Where(p => p.Date >= DateTime.Today && p.Date <= date);

            var ingredient_list = new List<Ingredient>();
            foreach (var plan in plans)
            {
                var ingredient = _context.Ingredient.Where(i => i.MealId == plan.Meal.Id);
                ingredient_list.AddRange(ingredient.ToList());
            }
            ingredient_list.OrderBy(i => i.Food);
            return View(ingredient_list);
        }
    }
}