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
        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "日付")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "時間帯")]
        public int Time { get; set; }

        [Required]
        [Display(Name = "料理名")]
        public int MealId { get; set; }
        public virtual Meal Meal { get; set; }
    }
}
