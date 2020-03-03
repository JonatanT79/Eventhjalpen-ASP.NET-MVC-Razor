using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EVTHJÄLPEN.Models;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;

namespace EVTHJÄLPEN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Recipe r = new Recipe();
            Random rng = new Random(DateTime.Now.Day);
            int recRecepie = rng.Next(1, 7);

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var query = from e in ctx.Recipe
                            where e.Id == recRecepie
                            select e;
                foreach (var item in query)
                {
                    r.Id = item.Id;
                    r.RecipeName = item.RecipeName;
                }

                return View(r);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Varukorg()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
