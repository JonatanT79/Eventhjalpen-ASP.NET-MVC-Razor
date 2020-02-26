using EVTHJÄLPEN.Models;
using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            EventDetails = new HashSet<EventDetails>();
            RecipeDetails = new HashSet<RecipeDetails>();
        }
        public int Id { get; set; }
        public int? RecipeTypeId { get; set; }
        public string RecipeName { get; set; }
        public int? EstimatedTime { get; set; }
        public virtual RecipeType RecipeType { get; set; }
        public virtual ICollection<RecipeSteps> RecipeSteps { get; set; }
        public virtual ICollection<EventDetails> EventDetails { get; set; }
        public virtual ICollection<RecipeDetails> RecipeDetails { get; set; }
    }
}
