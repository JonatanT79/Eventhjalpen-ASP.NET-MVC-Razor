using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EVTHJÄLPEN.Areas.Identity.Pages.Account.Manage
{


    public class AdressModel : PageModel
    {

        public int ID { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public Guid UserID { get; set; }
        
        public List<AdressModel> adressList = new List<AdressModel>();

        public IActionResult OnGet()
        {
            
            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TranbarDB;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connectionString);

            {

                SqlCommand cmd = new SqlCommand("SELECT ID, Adress, ZipCode, City, UserID FROM [UserAdress] WHERE UserID = @UserID", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@UserID", signedInUserID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                AdressModel adressModel = new AdressModel();


                while (rdr.Read())
                {
                    adressModel.ID = Convert.ToInt32(rdr["ID"]);
                    adressModel.Street = rdr["Adress"].ToString();
                    adressModel.ZipCode = Convert.ToInt32(rdr["ZipCode"]);
                    adressModel.City = rdr["City"].ToString();
                    adressModel.UserID = (Guid)rdr["UserID"];
                    adressList.Add(adressModel);
                }
                con.Close();
                return Page();
                
                
            }
            
        }

        

        public IActionResult OnPost(string Adress, int ZipCode, string City)
        {
            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string connectionString = "Server =(localdb)\\MSSQLLocalDB;Database=TranbarDB;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connectionString);
            
            string query = $@"INSERT INTO
                                [UserAdress]
                                    (

                                        Adress,
                                        ZipCode,
                                        City,
                                        UserID
                                    )
                                VALUES
                                    ( 

                                        @Adress,
                                        @ZipCode,
                                        @City,
                                        @UserID
                                        )";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;


            cmd.Parameters.AddWithValue("@Adress", Adress);
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@UserID", signedInUserID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return Page();
        }
    }
}

