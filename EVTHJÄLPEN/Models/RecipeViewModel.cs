using Eventhjälpen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class RecipeViewModel
    {
        public RecipeViewModel()
        {

        }
        public RecipeViewModel(Recipe r, RecipeType rt)
        {
            RecipeId = r.Id;
            RecipeName = r.RecipeName;
            EstimatedTime = r.EstimatedTime;
            RecipeTypeName = rt.RecipeTypeName;
        }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int? EstimatedTime { get; set; }
        public string RecipeTypeName { get; set; }

    }
}
