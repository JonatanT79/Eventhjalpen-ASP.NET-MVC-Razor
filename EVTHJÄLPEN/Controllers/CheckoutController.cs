using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EVTHJÄLPEN.Models;

namespace EVTHJÄLPEN.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult ViewCart(CartVM vm)
        {
            return View(vm);
        }
    }
}