using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class Orderhistory
    {
        public int OrderID { get; set; }
        public int? SumToPay { get; set; }
        public DateTime? Date { get; set; }
    }
}
