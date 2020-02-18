using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Eventhjälpen.Models
{
    public class DbAccess
    {
            
            SqlConnection con = new SqlConnection("Server = (localdb)\\MSSQLLocalDB; Database = TranbarDB; Trusted_Connection = True;");
            public string AddUserRecord(Users user)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO
                                [Users]
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
                                    )", con);
                    
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", user.Lastname);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Phonenumber", user.Phonenumber);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                return "Data saved..";
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        
                        con.Close();
                    
                    }
                    return ex.Message.ToString();
            }
            }
        }
}
