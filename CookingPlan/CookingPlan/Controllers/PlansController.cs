using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookingPlan.Data;
using CookingPlan.Models;
using Microsoft.AspNetCore.Authorization;
using CookingPlan.Others;

namespace CookingPlan.Controllers
{
    [Authorize]
    public class PlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Plan
                .Include(p => p.Meal)
                .Where(p => p.UserId == User.Identity.Name)
                .OrderBy(p => p.Date)
                .ThenBy(p => p.Time);

            ViewData["Time"] = Const.TimeList();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Plans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        /*
        // GET: Plans/Create
        public IActionResult Create()
        {
            var plan = new Plan() { UserId = 1 };
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "Name");
            return View(plan);
        }*/

        public IActionResult Create(int? mealId)
        {
            Plan plan;
            if (mealId.HasValue)
                plan = new Plan() { MealId = mealId.Value, UserId = User.Identity.Name, Date = DateTime.Today };
            else
                plan = new Plan() { UserId = User.Identity.Name, Date = DateTime.Today };

            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "Name");
            
            ViewData["Time"] = new SelectList(Const.TimeSelectList(), "Value", "Text");
            return View(plan);
        }

        // POST: Plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Date,Time,MealId")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "Name", plan.MealId);
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(int? id, int? mealId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            else if(plan.UserId != User.Identity.Name)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "Name", plan.MealId);
            if (mealId.HasValue)
                plan.MealId = mealId.Value;
            ViewData["Time"] = new SelectList(Const.TimeSelectList(), "Value", "Text");
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date,Time,MealId")] Plan plan)
        {
            if (id != plan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.Set<Meal>(), "Id", "Name", plan.MealId);
            return View(plan);
        }

        // GET: Plans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.Meal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plan.FindAsync(id);
            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.Id == id);
        }

        /*
        private List<string> GetIngredients(int MealId)
        {
            var ingredientList = new List<string>();
            var meal = _context.Meal.Find(MealId);
            foreach (var ingredient in meal.Ingredients)
            {
                var food = _context.Food.Find(ingredient.FoodId).Name;
                ingredientList.Add(food);
            }

            return ingredientList;
        }*/
    }
}
