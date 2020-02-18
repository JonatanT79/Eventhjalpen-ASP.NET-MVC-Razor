using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Eventhjälpen.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the EventhjalpenUser class
    public class EventhjalpenUser : IdentityUser
    {
        
        [Required]
        [PersonalData]
        public string Firstname { get; set; }
        [Required]
        [PersonalData]
        public string Lastname { get; set; }
        [Required]
        [PersonalData]
        public string Email { get; set; }
        [Required]
        [PersonalData]
        public string Phonenumber { get; set; }
    }
}
