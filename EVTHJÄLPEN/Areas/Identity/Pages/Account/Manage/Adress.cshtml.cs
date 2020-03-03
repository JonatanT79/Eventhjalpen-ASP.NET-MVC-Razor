using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EVTHJÄLPEN.Areas.Identity.Pages.Account.Manage
{
    public class AdressModel : PageModel
    {

        public int ID { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

        public void OnGet(int AdressId)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TranbarDB;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connectionString);
            {


                SqlCommand cmd = new SqlCommand("SELECT ID, Adress, ZipCode, City FROM [UserAdress] WHERE ID = @AdressId", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@AdressId", AdressId);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                AdressModel adressModel = new AdressModel();


                while (rdr.Read())
                {
                    adressModel.ID = Convert.ToInt32(rdr["AdressId"]);
                    adressModel.Street = rdr["Street"].ToString();
                    adressModel.ZipCode = Convert.ToInt32(rdr["ZipCode"]);
                    adressModel.City = rdr["City"].ToString();
                }


                con.Close();
            }
        }
        public IActionResult OnPost(string Adress, int ZipCode, string City)
        {

            string connectionString = "Server =(localdb)\\MSSQLLocalDB;Database=TranbarDB;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connectionString);

            string query = $@"INSERT INTO
                                [UserAdress]
                                    (

                                        Adress,
                                        ZipCode,
                                        City
                                    )
                                VALUES
                                    ( 

                                        @Adress,
                                        @ZipCode,
                                        @City
                                    )";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;


            cmd.Parameters.AddWithValue("@Adress", Adress);
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
            cmd.Parameters.AddWithValue("@City", City);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("./Adress");
        }
    }
}

