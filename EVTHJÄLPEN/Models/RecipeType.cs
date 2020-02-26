using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class RecipeType
    {
        public RecipeType()
        {
            Recipe = new HashSet<Recipe>();
        }
        public int Id { get; set; }
        public string RecipeTypeName { get; set; }
        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
