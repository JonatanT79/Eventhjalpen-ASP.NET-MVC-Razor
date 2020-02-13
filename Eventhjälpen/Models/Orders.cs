﻿using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class Orders
    {
        public Orders()
        {
            Orderdetails = new HashSet<Orderdetails>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? SumToPay { get; set; }
        public DateTime? CurrentDate { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
    }
}
