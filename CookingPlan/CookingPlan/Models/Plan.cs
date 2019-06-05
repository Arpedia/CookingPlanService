using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPlan.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
        public int MealId { get; set; }
    }
}
