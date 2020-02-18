using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventhjälpen.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phonenumber { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
