using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Eventhjälpen.Models
{

    public partial class AddUser : TranbarDBOContext
    {

        public static string connectionString = @"Server=(localdb)\\MSSQLLocalDB;Database=TranbarDB;Trusted_Connection=True;";
        public void SaveUser(Users Users)

        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $@"INSERT INTO
                                [User]
                                    (
                                        Firstname,
                                        Lastname,
                                        Email,
                                        Phonenumber
                                    )
                                VALUES
                                    (
                                        @Firstname,
                                        @Lastname,
                                        @Email,
                                        @Phonenumber
                                    )";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Firstname", Users.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", Users.Lastname);
                cmd.Parameters.AddWithValue("@Email", Users.Email);
                cmd.Parameters.AddWithValue("@Phonenumber", Users.Phonenumber);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        }
    }


