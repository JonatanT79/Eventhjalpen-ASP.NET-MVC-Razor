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

        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        public IActionResult Recipes(int id)
        {
            List<Recipe> _recepies = new List<Recipe>();
            List<RecipeType> _recipeTypes = new List<RecipeType>();
            RecipeVM vm = new RecipeVM();

            if (id <= 0)
            {
                try
                {
                    using (ApplicationDbContext ctx = new ApplicationDbContext())
                    {
                        _recepies = ctx.Recipe
                            .ToList();
                        _recipeTypes = ctx.RecipeType
                            .ToList();
                    }

                    vm.recipes = _recepies;
                    vm.recipeTypes = _recipeTypes;

                    return View(vm);
                }
                catch (Exception e)
                {
                    return Content("Failed loading recepies: " + e);
                }
            }

            try
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    _recipeTypes = ctx.RecipeType
                        .ToList();
                    _recepies = ctx.Recipe.Where(x => x.RecipeTypeId == id)
                        .ToList();
                }

                vm.recipes = _recepies;
                vm.recipeTypes = _recipeTypes;

                return View(vm);
            }
            catch (Exception e)
            {
                return Content("Failed loading recepies: " + e);
            }
        }

        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        public IActionResult ViewRecipe(int id, int portion = 4)
        {
            if (portion < 1)
            {
                portion = 1;
            }
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
                    SI.ProductName = rdr.GetString(2);
                    SI.ProductQuantity = rdr.GetDecimal(3);
                    SI.Measurement = rdr.GetString(4);
                    vp.Productslist.Add(SI);
                }
                con.Close();

                // Andra queryn -- Hämtar Stegnr och instruktioner

                con.Open();
                string SQL2 = @"select Stepnumber, Instructions from Recipe r
                                inner join RecipeSteps rs on rs.RecipeID = r.ID
                                where r.ID = @ID2";

                SqlCommand command = new SqlCommand(SQL2, con);
                command.Parameters.AddWithValue("@ID2", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    RecipeSteps RS = new RecipeSteps();
                    RS.Stepnumber = reader.GetInt32(0);
                    RS.Instructions = reader.GetString(1);
                    vp.StepList.Add(RS);
                }
                con.Close();
            }
            vp.Portion = portion;
            if (portion >= 1)
            {
                vp.Productslist.ForEach(pl =>
                  {
                      pl.ProductQuantity = portion * (pl.ProductQuantity * 1 / 4);
                  });
            };
            return View(vp);
        }

        [HttpPost]
        [Route("/[controller]/[action]")]
        public IActionResult OnPost([Bind("Product")] List<IngredientToCart> ic)
        {
            return RedirectToAction("ViewCart", "Checkout");
        }

        [HttpGet("{id}")]
        [Route("/[controller]/[action]")]
        public IActionResult RecipesByEvents(int id)
        {
            List<Recipe> _recepies = new List<Recipe>();
            List<RecipeType> _recipeTypes = new List<RecipeType>();

            RecipeVM vm = new RecipeVM();

            if (id <= 0)
            {
                using (ApplicationDbContext ctx = new ApplicationDbContext())
                {
                    _recepies = ctx.Recipe
                          .ToList();
                    _recipeTypes = ctx.RecipeType
                        .ToList();
                }
            }
            else
            {

                using (ApplicationDbContext ctx1 = new ApplicationDbContext())
                {
                    _recepies = ctx1.Recipe
                        .Where(e => e.EventDetails
                        .Any(r => r.EventId == id))
                        .ToList();
                    _recipeTypes = ctx1.RecipeType
                          .ToList();
                }
            }

            vm.recipes = _recepies;
            vm.recipeTypes = _recipeTypes;


            return View("~/Views/Recipe/Recipes.cshtml", vm);


        }
    }
}