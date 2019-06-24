using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPlan.Models
{
    public class Ingredient
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
        
        [Required]
        public string Food { get; set; }

        public string Num { get; set; }
    }
}
