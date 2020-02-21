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
        public IActionResult RecipeIndex()
        {
            List<RecipeViewModel> recipes = new List<RecipeViewModel>();
            using (SqlConnection con = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; Database = TranbarDB; Trusted_Connection = True;"))
            {
                con.Open();
                string SQL = @"Select r.ID, Recipename, EstimatedTime,rt.RecipeTypeName from recipe r
                               inner join RecipeType rt on r.RecipeTypeID = rt.ID";
                SqlCommand cmd = new SqlCommand(SQL, con);

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    RecipeViewModel r = new RecipeViewModel();
                    r.RecipeId = rdr.GetInt32(0);
                    r.RecipeName = rdr.GetString(1);
                    r.EstimatedTime = rdr.GetInt32(2);
                    r.RecipeTypeName = rdr.GetString(3);

                    recipes.Add(r);
                }
                con.Close();
            }
                return View(recipes);
        }

        public IActionResult ViewRecipe(int ID)
        {
            Recipe r = new Recipe();
            r.Id = ID;
            return View(r);
        }
    }
}