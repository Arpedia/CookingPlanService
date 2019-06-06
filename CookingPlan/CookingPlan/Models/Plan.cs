using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPlan.Models
{
    public class Plan
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
