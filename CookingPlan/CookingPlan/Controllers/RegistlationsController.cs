using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingPlan.Data;
using CookingPlan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookingPlan.Controllers
{
    [Route("api/[controller]")]
    public class RegistlationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RegistlationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody, Bind("UrlString, Number")] Url url)
        {
            var isExsist = _context.Meal.SingleOrDefault(m => m.Url == url.UrlString);
            if (isExsist != null)
                return Ok(isExsist.Id);
            else
            {
                var mealid = Registrations(url);
                var meal = await _context.Meal.FindAsync(mealid);
                return Ok(meal.Id);
            }
        }

        //private async Task<int> Registrations(Url url)
        private int Registrations(Url url)
        {
            Others.Scraping scraping = new Others.Scraping(url.UrlString, url.Number, _context);
            return scraping.Run().Result;
            //return await scraping.Run();

        }
        /*
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
