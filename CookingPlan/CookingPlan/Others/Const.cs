using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPlan.Others
{
    public static class Const
    {
        private static readonly string[] time_list = new string[] { "朝", "昼", "夕", "夜", "そのほか" };

        private static readonly SelectListItem[] time_selectlist = new SelectListItem[]
        {
            new SelectListItem() { Value = "1", Text = "朝"},
            new SelectListItem() { Value = "2", Text = "昼"},
            new SelectListItem() { Value = "3", Text = "夕"},
            new SelectListItem() { Value = "4", Text = "夜"},
            new SelectListItem() { Value = "5", Text = "そのほか"},
        };

        private static readonly string[] date_list = new string[] { "今日", "明日", "明後日", "4日後", "5日後" };

        public static string[] TimeList()
        {
            return time_list;
        }

        public static SelectListItem[] TimeSelectList()
        {
            return time_selectlist;
        }

        public static string[] DateList()
        {
            return date_list;
        }
    }
}
