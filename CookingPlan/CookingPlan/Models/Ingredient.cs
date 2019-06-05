using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPlan.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int IngredientId { get; set; }
        public string Num { get; set; }
    }
}
