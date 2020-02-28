using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using EVTHJÄLPEN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EVTHJÄLPEN.Controllers
{
    public class RecipeController : Controller
    {
        List<Products> prods = new List<Products>();

        [HttpGet]
        [Route("/[controller]/[action]")]
        public IActionResult Recipes()
        {
            List<Recipe> recepies = new List<Recipe>();
            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    recepies = ctx.Recipe
                        .ToList();
                }

                return View(recepies);
            }
            catch (Exception e)
            {
                return Content("Failed loading recepies: " + e);
            }
        }

        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        public IActionResult ViewRecipe(int id)
        {
            ViewProducts vp = new ViewProducts();

            using (SqlConnection con = new SqlConnection("Server=(localdb)\\Mssqllocaldb; Database= TranbarDB; MultipleActiveResultSets=true"))
            {
                con.Open();
                string SQL = @"select Recipename,EstimatedTime, ProductName, ProductQuantity,Measurement,RE.Id, PR.Id
                                from Products PR 
                                INNER JOIN RecipeDetails RD ON PR.ID = RD.ProductID 
                                INNER JOIN MeasurementUnit MU ON RD.MeasurementUnitID = MU.Id 
                                INNER JOIN Recipe RE ON RD.RecipeID = RE.ID  
                                where RE.Id = @ID";

                SqlCommand cmd = new SqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ShowIngrediens SI = new ShowIngrediens();
                    vp.RecipeID = rdr.GetInt32(5);
                    vp.RecipeName = rdr.GetString(0);
                    vp.EstimatedTime = rdr.GetInt32(1);
                    SI.ProductID = rdr.GetInt32(6);
                    SI.ProductName = rdr.GetString(2);
                    SI.ProductQuantity = rdr.GetDecimal(3);
                    SI.Measurement = rdr.GetString(4);
                    vp.Productslist.Add(SI);
                    
                }
                con.Close();
            }
            return View(vp);    
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public IActionResult OnPost([Bind("Product")] List<IngredientToCart> ic)
        {
            

            return RedirectToAction("ViewCart", "Checkout");
        }
    }
}