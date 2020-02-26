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
            List<ViewProducts> Productslist = new List<ViewProducts>();
            using (SqlConnection con = new SqlConnection("Server=(localdb)\\Mssqllocaldb; Database= TranbarDB; MultipleActiveResultSets=true"))
            {
                con.Open();
                String SQL = @"select ProductName, Quantity, ProductQuantity,Measurement, RecipeName
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
                    ViewProducts vp = new ViewProducts();
                    vp.ProductName = rdr.GetString(0);
                    vp.ProductQuantity = rdr.GetDecimal(2);
                    vp.Measurement = rdr.GetString(3);
                    Productslist.Add(vp);
                }
                con.Close();
            }
            return View(Productslist);

            //try
            //{
            //    using (ApplicationDbContext ctx = new ApplicationDbContext())
            //    {
            //        Recipe loadedRecipe = ctx.Recipe.FirstOrDefault(x => x.Id == id);
            //        return View(loadedRecipe);
            //    }
            //}
            //catch (Exception e)
            //{
            //    return Content("Failed loading recipe: " + e);
            //}
        }
    }
}