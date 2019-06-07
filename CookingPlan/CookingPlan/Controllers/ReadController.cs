using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingPlan.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReadController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
        [HttpGet]
        public async Task<JsonResult> Read()
        {
        }
        */
    }
}