using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Mvc;

namespace EVTHJÄLPEN.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult RecipeIndex()
        {
            List<Recipe> recipes = new List<Recipe>()
            {
                new Recipe() {Id = 1, RecipeName = "Kladdkaka", EstimatedTime = 45},
                new Recipe() {Id = 2, RecipeName = "Äpplepaj", EstimatedTime = 35},
                new Recipe() {Id = 3, RecipeName = "Frukttårta", EstimatedTime = 60}
            };
            return View(recipes);
        }

        public IActionResult ViewRecipe(int ID)
        {
            Recipe r = new Recipe();
            return View(r);
        }
    }
}