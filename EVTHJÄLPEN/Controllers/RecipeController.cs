using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using EVTHJÄLPEN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EVTHJÄLPEN.Controllers
{
    public class RecipeController : Controller
    {
        [HttpGet]
        [Route("/[controller]/[action]")]
        public IActionResult Recipes()
        {
            List<Recipe> recepies = new List<Recipe>();
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    recepies = ctx.Recipe
                        .ToList();
                }

                return View(recepies);
            }
            catch (Exception e)
            {
                return Content("Failed loading recepies: " + e);
            }
        }

        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        public IActionResult ViewRecipe(int id)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    Recipe loadedRecipe = ctx.Recipe.FirstOrDefault(x => x.Id == id);
                    return View(loadedRecipe);
                }
            }
            catch (Exception e)
            {
                return Content("Failed loading recipe: " + e);
            }
        }
    }
}