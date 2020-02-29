using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class ViewProductVM
    {
        public List<ViewProducts> vp { get; set;}
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public int? EstimatedTime { get; set; }
    }
}
