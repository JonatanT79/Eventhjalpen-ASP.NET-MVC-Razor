using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
