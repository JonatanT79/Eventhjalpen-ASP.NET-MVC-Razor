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

                    if(cookieString == "")
                    {
                        foreach (var item in ctx.Orderdetails)
                        {
                            ctx.Orderdetails.Remove(item);
                        }
                            ctx.SaveChanges();
                    }

                    var productIds = cookieString.Split(",").Select(c => int.Parse(c));

                    var Check = from e in ctx.Orderdetails
                                select e;

                    if (Check.Count() == 0)
                    {
                        var products = from e in ctx.Products
                                       where productIds.Contains(e.Id)
                                       select e;

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
                                        c.SaveChanges();
                                    }
                                }

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

                            var SetValue = from e in ctx.Products
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
                                foreach (var item in SetValue)
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
                    if (Apply != "Add" && Apply != "Remove")
                    {
                        Response.Cookies.Append("Varukorg", cookieString, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(60.0) });
                    }
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
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    var DeleteFromDatabase = from e in ctx.Orderdetails
                                             where e.ProductId == RemoveID
                                             select e;

                    foreach (var item in DeleteFromDatabase)
                    {
                        ctx.Orderdetails.Remove(item);
                    }
                    ctx.SaveChanges();

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

                    var Value = from e in ctx.Products
                                join e2 in ctx.Orderdetails on e.Id equals e2.ProductId
                                where productIds.Contains(e.Id)
                                select new
                                {
                                    PID = e.Id,
                                    PNA = e.ProductName,
                                    QUA = e.Quantity,
                                    PRI = e.Price,
                                    AMO = e2.Amount
                                };

                    foreach (var item in Value)
                    {
                        ShowIngrediens si = new ShowIngrediens();
                        si.ProductID = item.PID;
                        si.ProductName = item.PNA;
                        si.Quantity = item.QUA;
                        si.Price = item.PRI;
                        si.Amount = item.AMO;
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
// bugg; Om dataasen har data och cookiestrignen är tom kmr det bli en krash -- cookiestringen raderas efter 60 min men datan i databasen kvarstår
// kolla först om cookiestringen är tom, är den tom rensa databasen