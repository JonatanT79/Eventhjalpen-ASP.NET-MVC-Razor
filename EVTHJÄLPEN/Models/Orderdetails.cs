using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class Orderdetails
    {
        public int Id { get; set; }
        public int? OrdersId { get; set; }
        public int? ProductId { get; set; }
        public int Amount { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual Products Product { get; set; }
    }
}
