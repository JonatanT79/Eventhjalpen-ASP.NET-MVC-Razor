using EVTHJÄLPEN.Models;
using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class RecipeDetails
    {
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        public int? ProductId { get; set; }
        public decimal ProductQuantity { get; set; }
        public int MeasurementUnitID { get; set; }
        public virtual Products Product { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual MeasurementUnit MeasurementUnit { get; set; }
    }
}
