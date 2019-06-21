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
        [Display(Name = "料理名")]
        public string Name { get; set; }
        [Display(Name = "レシピ URL")]
        public string Url { get; set; }

        [Display(Name = "材料")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
