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
        public string Quantity { get; set; }
        public Decimal Price { get; set; }

    }
}
