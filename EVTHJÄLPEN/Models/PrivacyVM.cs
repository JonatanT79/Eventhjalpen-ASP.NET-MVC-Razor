using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class PrivacyVM
    {
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(100), EmailAddress]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Exceeded chararcter limit")]
        public string Message { get; set; }
    }
}
