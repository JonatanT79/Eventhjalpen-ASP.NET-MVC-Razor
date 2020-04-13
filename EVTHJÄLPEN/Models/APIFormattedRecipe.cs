using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Models;
using EVTHJÄLPEN.Data;

namespace EVTHJÄLPEN.Models
{
    public class APIFormattedRecipe
    {
        public APIFormattedRecipe(int id)
        {
            Id = id;
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                Steps = ctx.RecipeSteps.Where(x => x.RecipeID == id)
                    .OrderBy(x => x.Stepnumber)
                    .Select(x => x.Instructions)
                    .ToArray();
                RecipeName = ctx.Recipe.Where(x => x.Id == id).Select(x => x.RecipeName).FirstOrDefault();
                EstimatedTime = ctx.Recipe.Where(x => x.Id == id).Select(x => x.EstimatedTime).FirstOrDefault();

                Ingredients = new APIFormattedIngredient() {
                    Name = ctx.RecipeDetails.Where(x => x.RecipeId == id).Select(x => x.Product.ProductName).ToArray(),
                    Unit = ctx.RecipeDetails.Where(x => x.RecipeId == id).Select(x => x.MeasurementUnit.Measurement).ToArray(),
                    Amount = ctx.RecipeDetails.Where(x => x.RecipeId == id).Select(x => x.ProductQuantity).ToArray()
                };
            }
        }            

        public int Id { get; set; }
        public string RecipeName { get; set; }
        public int? EstimatedTime { get; set; }
        public string[] Steps { get; set; }
        public APIFormattedIngredient Ingredients { get; set; }
    }
}
