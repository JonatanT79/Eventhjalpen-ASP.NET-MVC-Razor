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
using Microsoft.AspNetCore.Identity;

namespace EVTHJÄLPEN.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
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

        public IActionResult Varukorg(int ID, string Empty, int RemoveID, int ProductID, string Apply)
        {
            ViewProducts vp = new ViewProducts();

            var varukorg = Request.Cookies.SingleOrDefault(c => c.Key == "Varukorg");

            if (RemoveID == 0)
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    string cookieString;
                    var recipeProductsIds = from e in ctx.RecipeDetails
                                            where e.RecipeId == ID
                                            select e.ProductId;

                    if (varukorg.Value != "" && varukorg.Value != null && ID != 0)
                    {
                        cookieString = varukorg.Value + "," + string.Join(",", recipeProductsIds);
                    }
                    else
                    {
                        cookieString = varukorg.Value + string.Join(",", recipeProductsIds);
                    }
                    var productIds = cookieString.Split(",").Select(c => int.Parse(c));

                    var Check = from e in ctx.Orderdetails
                            select e;

                    if (Check.Count() == 0)
                    {
                        var products = from e in ctx.Products
                                       where productIds.Contains(e.Id)
                                       select e;

                        int i = 0;
                        if (!cookieString.Equals(""))
                        {
                            foreach (var item in products)
                            {
                                Orderdetails od = new Orderdetails();
                                od.ProductId = item.Id;
                                od.Amount = 1;

                                using (ApplicationDbContext c = new ApplicationDbContext())
                                {
                                    if (!c.Orderdetails.Any(A => A.ProductId == item.Id))
                                    {
                                        ctx.Orderdetails.Add(od);
                                    }
                                    c.SaveChanges();
                                }

                                ShowIngrediens si = new ShowIngrediens();
                                si.ProductID = item.Id;
                                si.ProductName = item.ProductName;
                                si.Quantity = item.Quantity;
                                si.Price = item.Price;
                                si.Amount = 1;
                                vp.TotalSum += (decimal.ToDouble(si.Price) * si.Amount);
                                vp.Productslist.Add(si);
                                i++;
                            }
                        }
                    }
                    else
                    {
                        using (ApplicationDbContext c = new ApplicationDbContext())
                        {
                            int i = 0;
                            var modify = from m in c.Orderdetails
                                         where m.ProductId == ProductID
                                         select m;

                            if (i == 0 && Apply == "Add")
                            {
                                foreach (var items in modify)
                                {
                                    items.Amount++;
                                }
                            }
                            else if (i == 0 && Apply == "Remove")
                            {
                                foreach (var items in modify)
                                {
                                    items.Amount--;
                                }
                            }
                            c.SaveChanges();

                            foreach (var additem in productIds)
                            {
                                Orderdetails od = new Orderdetails() { ProductId = additem, Amount = 1 };
                                if (!c.Orderdetails.Any(A => A.ProductId == additem))
                                {
                                    ctx.Orderdetails.Add(od);
                                    ctx.SaveChanges();
                                }
                            }

                            var products = from e in ctx.Products
                                           join e2 in ctx.Orderdetails on e.Id equals e2.ProductId
                                           where productIds.Contains(e.Id)
                                           select new
                                           {
                                               PID = e.Id,
                                               PNA = e.ProductName,
                                               Qua = e.Quantity,
                                               Pri = e.Price,
                                               Amo = e2.Amount
                                           };

                            if (!cookieString.Equals(""))
                            {
                                foreach (var item in products)
                                {
                                    ShowIngrediens si = new ShowIngrediens();
                                    si.ProductID = item.PID;
                                    si.ProductName = item.PNA;
                                    si.Quantity = item.Qua;
                                    si.Price = item.Pri;
                                    si.Amount = item.Amo;
                                    vp.TotalSum += (decimal.ToDouble(si.Price) * si.Amount);
                                    vp.Productslist.Add(si);
                                    i++;
                                }
                            }
                        }
                    }
                    Response.Cookies.Append("Varukorg", cookieString, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(60.0) });
                    ctx.SaveChanges();
                }
            }

            if (Empty == "Empty")
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {

                    foreach (var item in ctx.Orderdetails)
                    {
                        ctx.Orderdetails.Remove(item);
                    }

                    ctx.SaveChanges();
                    Response.Cookies.Delete("Varukorg");
                    vp.Productslist.Clear();
                    vp.TotalSum = 0;

                    return View(vp);
                }
            }
            else if (RemoveID != 0)
            {
                //om man tar bort en produkt, rensa hela tabellen och lägg till nytt, spara modifierings värderna 
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    varukorg = Request.Cookies.SingleOrDefault(s => s.Key == "Varukorg");

                    string cookieString = "";

                    int index = varukorg.Value.IndexOf("," + RemoveID.ToString() + ",");

                    if (index == -1)
                    {
                        index = varukorg.Value.IndexOf(RemoveID.ToString());
                    }

                    if (varukorg.Value.Length == 1 || varukorg.Value.Length == 2)
                    {
                        Response.Cookies.Delete("Varukorg");
                        vp.Productslist.Clear();
                        vp.TotalSum = 0;
                        return View(vp);
                    }

                    if (RemoveID.ToString().Length > 1)
                    {
                        // checks if the 2-number is last on string
                        if (index == (varukorg.Value.Length - 2))
                        {
                            cookieString = varukorg.Value.Remove(index - 1, 3);
                        }
                        else
                        {
                            cookieString = varukorg.Value.Remove(index, 3);
                        }
                    }
                    else
                    {
                        if (index == (varukorg.Value.Length - 1))
                        {
                            cookieString = varukorg.Value.Remove(index - 1, 2);
                        }
                        else
                        {
                            cookieString = varukorg.Value.Remove(index, 2);
                        }
                    }

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
                        //   si.Amount = Amount;
                        vp.TotalSum += (decimal.ToDouble(si.Price) * si.Amount);
                        vp.Productslist.Add(si);
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
