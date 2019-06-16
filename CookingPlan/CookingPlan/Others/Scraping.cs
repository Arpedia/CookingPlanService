using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CookingPlan.Data;
using CookingPlan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CookingPlan.Others
{
    public class Scraping
    {
        private HttpClient client = new HttpClient();
        private ApplicationDbContext context;
        private string url;
        private int partialNum;

        public Scraping(string urlstring, int cardNum, ApplicationDbContext _context)
        {
            url = urlstring;
            partialNum = cardNum;
            context = _context;
        }

        public async Task<int> Run()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var doc = GetHtmlDoc().Result;
            var id = AddMeal(doc);
            AddFood(doc);
            await AddIngredient(doc, id);
            return id;
        }

        private async Task<IHtmlDocument> GetHtmlDoc()
        {
            var response = await client.GetAsync(url);
            var source = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser();
            return await parser.ParseDocumentAsync(source);
        }

        private int AddMeal(IHtmlDocument doc)
        {

            var title = doc.GetElementsByClassName("recipeTitle").Select(item =>
            {
                return item.TextContent.Trim();
            }).First();

            var ExistMeal = context.Meal.SingleOrDefault(m => m.Url == url);
            if (ExistMeal != null) return ExistMeal.Id;

            Meal meal = new Meal { Name = title, Url = url };
            context.Add(meal);
            context.SaveChanges();
            return context.Meal.Single(m => m.Url == url).Id;
        }

        private int AddFood(IHtmlDocument doc)
        {
            var listItems = doc.GetElementById("recipeMaterialList")
                .GetElementsByTagName("dl")
                .Select(item =>
                {
                    var ingredient = item.QuerySelector("dt").TextContent.Trim();
                    var num = item.QuerySelector("dd").TextContent.Trim();
                    return new { Ingredient = ingredient, Num = num };
                });
            
            listItems.ToList().ForEach(item =>
            {
                var isExist = context.Food.Count(m => m.Name == item.Ingredient);
                if (isExist > 0) return;
                Food food = new Food { Name = item.Ingredient };
                context.Add(food);
            });
            return context.SaveChanges();
        }

        private async Task<int> AddIngredient(IHtmlDocument doc, int mealId)
        {
            if (context.Ingredient.Count(m => m.MealId == mealId) > 0)
                return 0;

            var listItems = doc.GetElementById("recipeMaterialList")
                .GetElementsByTagName("dl")
                .Select(item =>
                {
                    var ingredient = item.QuerySelector("dt").TextContent.Trim();
                    var num = item.QuerySelector("dd").TextContent.Trim();
                    return new { Ingredient = CheckIngredientName(ingredient), Num = num };
                });

            listItems.ToList().ForEach(item => {
                var foodid = context.Food.Single(m => m.Name == item.Ingredient).Id;
                var ingredient = new Ingredient { MealId = mealId, FoodId = foodid, Num = item.Num };
                context.Add(ingredient);
            });
            return await context.SaveChangesAsync();

        }

        private string CheckIngredientName(string source)
        {
            string tmp = source;
            if (Regex.IsMatch(source, ".「(.+)*」"))
            {
                tmp = source.Substring(2, source.Length - 2); // A「OO...のA「の部分を削除
                tmp = tmp.Remove(tmp.IndexOf('」'), 1);
            }
            else if(Regex.IsMatch(source, "「(.+)*」"))
            {
                tmp = source.Remove(tmp.IndexOf('「'), 1);
                tmp = tmp.Remove(tmp.IndexOf('」'), 1);
            }

            return tmp;
        }
    }
}
