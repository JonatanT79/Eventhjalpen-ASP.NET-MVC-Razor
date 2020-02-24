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
        public IActionResult RecipeIndex()
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

        [HttpGet]
        public IActionResult ViewRecipe(int ID)
        {
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    Recipe loadedRecipe = ctx.Recipe.FirstOrDefault(x => x.Id == ID);
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