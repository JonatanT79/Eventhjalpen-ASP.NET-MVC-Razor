using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EVTHJÄLPEN.Models;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EVTHJÄLPEN.Controllers
{
    [ApiController]
    [Route("api")]
    public class RecipeAPIController : ControllerBase
    {
        [HttpGet]
        public List<Recipe> GetAllRecipes()
        {
            RecipeVM vm = new RecipeVM();
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var query = from e in ctx.Recipe
                            select e;

                vm.recipes = query.ToList();
            }
            return vm.recipes;
        }
        [HttpGet("Recipe/{id}")]
        public ActionResult<string> Get(int id)
        {
            var result = new APIFormattedRecipe(id);
            if (result != null && id < 7)
            {
                string json = JsonConvert.SerializeObject(result, Formatting.None);
                json = JsonConvert.SerializeObject(result, Formatting.Indented);
                return json;
            }
            else
            {
                return "Receptet med det ID finns inte" ;
            }

        }
    }
}