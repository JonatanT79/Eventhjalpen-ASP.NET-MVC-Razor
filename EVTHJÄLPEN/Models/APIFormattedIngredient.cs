using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class APIFormattedIngredient
    {
        public string[] Name { get; set; }
        public decimal[] Amount { get; set; }
        public string[] Unit { get; set; }
    }
}
