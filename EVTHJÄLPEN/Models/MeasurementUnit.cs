using Eventhjälpen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class MeasurementUnit
    {
        public int ID { get; set; }
        public string Measurement { get; set; }
        public virtual ICollection<RecipeDetails> RecipeDetails { get; set; }
    }
}
