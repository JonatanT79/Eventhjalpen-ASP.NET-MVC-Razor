using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class RecipeDetails
    {
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        public int? ProductId { get; set; }

        public virtual Products Product { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
