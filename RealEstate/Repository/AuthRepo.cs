using RealEstate.Models;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RealEstate.Repository
{
    public class AuthRepo
    {
 
       public void CreateResetPassword(ResetPassword model)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            { 
               conn.Open();
                string query = "INSERT INTO ResetPassword (Token,User_id,Expires) VALUES (@Token , @User_Id,@Expires)";

                using(SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@User_Id", model.UserId);
                    cmd.Parameters.AddWithValue("@Token", model.Token);
                    cmd.Parameters.AddWithValue("@Expires", model.TokenExpires);
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public void VerifyTokenPassword(string token,string email )
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT rp.Id FROM ResetPassword rp JOIN Users u ON rp.User_id = u.Id WHERE u.Email = @Email AND rp.Token = @Token AND rp.Expires > GETDATE()";
                using(SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Token", token);
                    int? reader =  (int)cmd.ExecuteScalar();
                    if(reader ==null)
                    {
                        throw new Exception("Token không hợp lệ hoặc đã hết hạn");
                    }
                }
            }
        }
    }
}