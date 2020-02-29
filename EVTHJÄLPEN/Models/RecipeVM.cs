using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;

namespace EVTHJÄLPEN.Models
{
    public class RecipeVM
    {
        public List<Recipe> recipes {get; set;}
        public List<RecipeType> recipeTypes { get; set; }
    }
}
