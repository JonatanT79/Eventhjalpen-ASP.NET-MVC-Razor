using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Models
{
    public class ViewProducts
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public int EstimatedTime { get; set; }
        public int Portion { get; set; }
        public double TotalSum { get; set; }
        public string StatusMessage { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }

        public List<ShowIngrediens> Productslist = new List<ShowIngrediens>();

        public List<RecipeSteps> StepList = new List<RecipeSteps>();
    }
}
