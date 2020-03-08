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
using System.Data.SqlClient;
using System.Data.Entity;

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
        public IActionResult Varukorg(int ID, string Empty, int RemoveID)
        {
            // if cookiestring > varukorg.value
            ViewProducts vp = new ViewProducts();

            var varukorg = Request.Cookies.SingleOrDefault(c => c.Key == "Varukorg");

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var recipeProductsIds = from e in ctx.RecipeDetails
                                        where e.RecipeId == ID
                                        select e.ProductId;

                string cookieString = varukorg.Value + string.Join(",", recipeProductsIds);
                var productIds = cookieString.Split(",").Select(c => int.Parse(c));

                var products = from e in ctx.Products
                               where productIds.Contains(e.Id)
                               select e;

                if (!cookieString.Equals(""))
                {
                    foreach (var item in products)
                    {
                        ShowIngrediens si = new ShowIngrediens();
                        si.ProductID = item.Id;
                        si.ProductName = item.ProductName;
                        si.Quantity = item.Quantity;
                        si.Price = item.Price;
                        si.Amount = 1;
                        vp.TotalSum += (decimal.ToDouble(si.Price) * si.Amount);
                        vp.Productslist.Add(si);
                    }
                }
                if (RemoveID == 0)
                {
                    Response.Cookies.Append("Varukorg", cookieString, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(60.0) });
                }
            }

            if (Empty == "Empty")
            {
                Response.Cookies.Delete("Varukorg");
                vp.Productslist.Clear();
                vp.TotalSum = 0;
                return View(vp);
            }
            else if (RemoveID != 0)
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    var search = vp.Productslist.SingleOrDefault(c => c.ProductID == RemoveID);
                    vp.Productslist.Remove(search);

                    var i = search;
                    var filter = from e in vp.Productslist
                                 select e.ProductID;

                    string cookieString = varukorg.Value.Replace(varukorg.Value, "") + string.Join(",", filter);
                    var productIds = cookieString.Split(",").Select(c => int.Parse(c));

                    var products = from e in ctx.Products
                                   where productIds.Contains(e.Id)
                                   select e;

                    foreach (var item in products)
                    {
                        ShowIngrediens si = new ShowIngrediens();
                        si.ProductID = item.Id;
                        si.ProductName = item.ProductName;
                        si.Quantity = item.Quantity;
                        si.Price = item.Price;
                        si.Amount = 1;
                        vp.TotalSum += (decimal.ToDouble(si.Price) * si.Amount);
                    }
                    Response.Cookies.Append("Varukorg", cookieString, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(60.0) });
                }

                return View(vp); 
            }

            return View(vp);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
