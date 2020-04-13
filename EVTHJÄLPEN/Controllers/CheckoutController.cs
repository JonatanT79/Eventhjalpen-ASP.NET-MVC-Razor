using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EVTHJÄLPEN.Models;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using System.Security.Claims;

namespace EVTHJÄLPEN.Controllers
{
    public class CheckoutController : Controller
    {
        public int OrderID { get; set; }
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult ViewCart()
        {
            ViewProducts vp = new ViewProducts();
            AddListValue(vp);
            
            return View(vp);
            
        }
        public IActionResult DoneOrder(string UserID, int SumToPay)
        {

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ViewProducts vp = new ViewProducts();
                Orders o = new Orders();
                o.AspUserId = UserID;
                o.CurrentDate = DateTime.Now;
                o.SumToPay = SumToPay;

                ctx.Orders.Add(o);
                ctx.SaveChanges();
                OrderID = o.Id;


                ctx.SaveChanges();
                Response.Cookies.Delete("Varukorg");
                vp.Productslist.Clear();
                vp.TotalSum = 0;
            }
        

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var updateod = from e in ctx.Orderdetails
                               select e;

                foreach (var item in updateod)
                {
                    item.OrdersId = OrderID;
                }
                ctx.SaveChanges();
            }
            return View();
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
                    vp.Email = User.FindFirstValue(ClaimTypes.Name);

                    vp.Productslist.Add(si);
                }
            }  
        }
    }
}