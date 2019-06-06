using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPlan.Models
{
    public class Meal
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
