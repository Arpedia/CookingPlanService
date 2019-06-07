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
            var id = await AddMeal(doc);
            await AddFood(doc);
            return id;
        }

        private async Task<IHtmlDocument> GetHtmlDoc()
        {
            var response = await client.GetAsync(url);
            var source = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser();
            return await parser.ParseDocumentAsync(source);
        }

        private async Task<int> AddMeal(IHtmlDocument doc)
        {

            var title = doc.GetElementsByClassName("recipeTitle").Select(item =>
            {
                return item.TextContent.Trim();
            }).First();

            var ExistMeal = context.Meal.SingleOrDefault(m => m.Url == url);
            if (ExistMeal != null) return ExistMeal.Id;

            Meal meal = new Meal { Name = title, Url = url };
            context.Add(meal);
            await context.SaveChangesAsync();
            return context.Meal.Single(m => m.Url == url).Id;
        }

        private async Task<int> AddFood(IHtmlDocument doc)
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
                var isExist = context.Food.Select(m => m.Name == item.Ingredient).Count();
                if (isExist > 0) return;
                Food food = new Food { Name = item.Ingredient };
                context.Add(food);
            });

            return await context.SaveChangesAsync();
        }
    }
}
