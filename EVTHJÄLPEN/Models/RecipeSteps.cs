using Eventhjälpen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class RecipeSteps
    {
        public int ID { get; set; }
        public int Stepnumber { get; set; }
        public string Instructions { get; set; }
        public int RecipeID { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
