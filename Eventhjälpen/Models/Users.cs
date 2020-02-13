using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
