using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Authorization;
namespace Eventhjälpen.Controllers
{
    public class HomeController : Controller
    {
        DbAccess dba = new DbAccess();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult RegisterUser([Bind] Users users)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dba.AddUserRecord(users);
                    TempData["msg"] = resp;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
