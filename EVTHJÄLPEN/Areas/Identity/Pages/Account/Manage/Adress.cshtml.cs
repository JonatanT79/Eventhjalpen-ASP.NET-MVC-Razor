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
        public string CareOf { get; set; }
        public string City { get; set; }
        public Guid UserID { get; set; }

        public List<AdressModel> adressList = new List<AdressModel>();
        public bool isAdressEmpty;
        public string query = "";
        public IActionResult OnGet()
        {

            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TranbarDB;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connectionString);

            {

                SqlCommand cmd = new SqlCommand("SELECT ID, Adress, ZipCode, CareOf, City, UserID FROM [UserAdress] WHERE UserID = @UserID", con);
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
                    adressModel.CareOf = rdr["CareOf"].ToString();
                    adressModel.City = rdr["City"].ToString();
                    adressModel.UserID = (Guid)rdr["UserID"];
                    adressList.Add(adressModel);
                    if (adressList.Count < 1)
                    {
                        isAdressEmpty = true;
                    }
                }
                con.Close();
                return Page();


            }

        }




        public IActionResult OnPost(string Adress, string CareOf, int ZipCode, string City)
        {
            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string connectionString = "Server =(localdb)\\MSSQLLocalDB;Database=TranbarDB;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(connectionString);

            if (isAdressEmpty == true)
            {
                query = $@"INSERT INTO
                                [UserAdress]
                                    (

                                        Adress,
                                        CareOf,
                                        ZipCode,
                                        City,
                                        UserID
                                    )
                                VALUES
                                    ( 

                                        @Adress,
                                        @CareOf,
                                        @ZipCode,
                                        @City,
                                        @UserID
                                        )";

            }
            else
            {
                query = $@" UPDATE [UserAdress]
                                SET Adress = @Adress,
                                CareOf = @CareOf,
                                ZipCode = @ZipCode,
                                City = @City
                                
                                Where UserID = @UserID
                                ";
            }

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;


            cmd.Parameters.AddWithValue("@Adress", Adress);
            cmd.Parameters.AddWithValue("@CareOf", CareOf);
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

