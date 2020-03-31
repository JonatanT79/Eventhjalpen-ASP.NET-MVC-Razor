using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EVTHJÄLPEN.Data;
using EVTHJÄLPEN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVTHJÄLPEN.Areas.Identity.Pages.Account.Manage
{
    public class OrderhistoryModel : PageModel
    {
        public List<Orderhistory> OrderDetailsList = new List<Orderhistory>();

        public List<ShowIngrediens> OrderProductList = new List<ShowIngrediens>();

        public void OnGet()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var Orderhistory = from e in ctx.Orders
                                   join e2 in ctx.Orderdetails on e.Id equals e2.OrdersId
                                   join e3 in ctx.Products on e2.ProductId equals e3.Id
                                   where e.AspUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                   orderby e.CurrentDate descending
                                   select new
                                   {
                                       OID = e.Id,
                                       Sum = e.SumToPay,
                                       Date = e.CurrentDate,
                                       PNA = e3.ProductName,
                                       QUA = e3.Quantity,
                                       PRI = e3.Price,
                                       AMO = e2.Amount
                                   };

                foreach (var item in Orderhistory)
                {
                    Orderhistory oh = new Orderhistory();
                    ShowIngrediens si = new ShowIngrediens();

                    oh.OrderID = item.OID;
                    oh.SumToPay = item.Sum;
                    oh.Date = item.Date;
                    si.ProductName = item.PNA;
                    si.Quantity = item.QUA;
                    si.Price = item.PRI;
                    si.Amount = item.AMO;

                    OrderProductList.Add(si);
                    OrderDetailsList.Add(oh);
                }
            }
        }
    }
}
