using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class EventDetails
    {
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        public int? EventId { get; set; }

        public virtual Events Event { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
