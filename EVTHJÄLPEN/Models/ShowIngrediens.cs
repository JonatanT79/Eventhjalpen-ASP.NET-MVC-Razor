using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class ShowIngrediens
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductQuantity { get; set; }
        public string Measurement { get; set; }
    }
}
