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
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using EVTHJÄLPEN.Services;

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

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(PrivacyVM vm)
        {
            if (ModelState.IsValid)
            {
                EmailSender es = new EmailSender(); 
                Email mailToSend = new Email
                {
                    subj = "Nytt kundmeddelande",
                    to = "evthjalpen@gmail.com",
                    body = $"<br><b>Kundnamn</b>: " + vm.Name +
                        $"<br><b>Kundmail</b>: " + vm.Email +
                        $"<br>" +
                        $"<br><b>Meddelande</b>" + 
                        $"<br>" + vm.Message
                };
                es.SendEmail(mailToSend); 

                return RedirectToAction("Privacy");
            }
            else
            {
                return Index();
            }
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

                    if (Apply == "Add" || Apply == "Remove")
                    {
                        cookieString = varukorg.Value;
                    }
                    else if (varukorg.Value != "" && varukorg.Value != null && ID != 0)
                    {
                        cookieString = varukorg.Value + "," + string.Join(",", recipeProductsIds);
                    }
                    else
                    {
                         cookieString = varukorg.Value + string.Join(",", recipeProductsIds);
                    }

                    if (cookieString == "")
                    {
                        RemoveFromOrderdetails();
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
                                        c.Orderdetails.Add(od);
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
                                vp.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                                vp.Productslist.Add(si);
                            }
                        }
                    }
                    else
                    {
                        using (ApplicationDbContext c = new ApplicationDbContext())
                        {
                            var modify = from m in c.Orderdetails
                                         where m.ProductId == ProductID
                                         select m;

                            if (Apply == "Add")
                            {
                                foreach (var items in modify)
                                {
                                    items.Amount++;
                                }
                            }
                            else if (Apply == "Remove")
                            {
                                foreach (var items in modify)
                                {
                                    items.Amount--;

                                    if (items.Amount == 0)
                                    {
                                        RemoveItem(ProductID, vp);
                                        vp.StatusMessage = "Produkten har tagits bort";
                                        return View(vp);
                                    }
                                }
                            }
                            c.SaveChanges();

                            foreach (var additem in productIds)
                            {
                                Orderdetails od = new Orderdetails() { ProductId = additem, Amount = 1 };
                                if (!c.Orderdetails.Any(A => A.ProductId == additem))
                                {
                                    c.Orderdetails.Add(od);
                                    c.SaveChanges();
                                }
                            }
                        }

                        AddListValue(vp);
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
                RemoveItem(RemoveID, vp);
            }
            return View(vp);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Methods ---------------------------------------------------------------------------------------------------------
        public ViewResult RemoveItem(int RemoveID, ViewProducts vp)
        {
            var varukorg = Request.Cookies.SingleOrDefault(s => s.Key == "Varukorg");

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

                    // return View(vp) hoppar direkt ner till sista måsvingen i metoden
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

                AddListValue(vp);
                Response.Cookies.Append("Varukorg", cookieString, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(60.0) });
            }
            return View(vp);
        }
        public void AddListValue(ViewProducts vp)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var SetValue = from e in ctx.Products
                               join e2 in ctx.Orderdetails on e.Id equals e2.ProductId
                               select new
                               {
                                   PID = e.Id,
                                   PNA = e.ProductName,
                                   Qua = e.Quantity,
                                   Pri = e.Price,
                                   Amo = e2.Amount
                               };

                foreach (var item in SetValue)
                {
                    ShowIngrediens si = new ShowIngrediens();
                    si.ProductID = item.PID;
                    si.ProductName = item.PNA;
                    si.Quantity = item.Qua;
                    si.Price = item.Pri;
                    si.Amount = item.Amo;
                    vp.TotalSum += (decimal.ToDouble(si.Price) * si.Amount);
                    vp.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    vp.Productslist.Add(si);
                }
            }
        }
        public void RemoveFromOrderdetails()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var delete = from e in ctx.Orderdetails
                             select e;

                foreach (var item in delete)
                {
                    ctx.Orderdetails.Remove(item);
                }
                ctx.SaveChanges();
            }
        }
    }
}
