using Eventhjälpen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class UserAdress
    {
        public int ID { get; set; }
        public string Adress { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public Guid UserID { get; set; }
    }
}
