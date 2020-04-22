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

        public void OnGet()
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var Orderhistory = from e in ctx.Orders
                                   where e.AspUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                   orderby e.CurrentDate descending
                                   select e;

                if(Orderhistory != null)
                {
                    foreach (var item in Orderhistory)
                    {
                        Orderhistory oh = new Orderhistory();

                        oh.OrderID = item.Id;
                        oh.SumToPay = item.SumToPay;
                        oh.Date = item.CurrentDate;

                        OrderDetailsList.Add(oh);
                    }
                }
            }
        }
    }
}
