using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EVTHJÄLPEN.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult RecipeIndex()
        {
            List<Recipe> recipes = new List<Recipe>();
            using (SqlConnection con = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; Database = TranbarDB; Trusted_Connection = True;"))
            {
                con.Open();
                string SQL = @"select r.ID, Recipename, EstimatedTime,rt.RecipeTypeName from recipe r
                               inner join RecipeType rt on r.RecipeTypeID = rt.ID";
                SqlCommand cmd = new SqlCommand(SQL, con);

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    string RecipeType = "";
                    Recipe r = new Recipe();
                    r.Id = rdr.GetInt32(0);
                    r.RecipeName = rdr.GetString(1);
                    r.EstimatedTime = rdr.GetInt32(2);
                    RecipeType = rdr.GetString(3);
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