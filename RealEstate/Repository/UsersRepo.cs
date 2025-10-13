using RealEstate.Models;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace RealEstate.Repository
{

   
    public class UsersRepo
    {
       
        public User VerifyLogin (string Email)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("VerifyLogin", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 50).Value = Email;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        return new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            UserName = Convert.ToString(reader["Name"]),
                            Email = Convert.ToString(reader["Email"]),
                            Password = Convert.ToString(reader["Password"]),
                            Phone = Convert.ToString(reader["Phone"]),
                            Avatar = Convert.ToString(reader["Avatar"]),
                            Role = new Role()
                            {
                                Id = Convert.ToInt32(reader["RoleId"]),
                                Name = Convert.ToString(reader["Role"])
                            }

                        };

                    }
                }
            }
            return null;
        }
        public int FindEmail (string Email)
        {
            int? result;
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open ();
                string query = "SELECT Id FROM Users WHERE Email = @Email";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 50).Value = Email;
                    cmd.Parameters.AddWithValue("@Email", Email);
                    result =Convert.ToInt32(cmd.ExecuteScalar());
                    //cmd.ExecuteScalar();

                }



            }
            if (result == 0) return -1;

            return (int)result ;
        }
        public User CreateUser(string Email ,string Name,  string Password)
        {
            User user = null;
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

              string query = "INSERT INTO Users (Email,Name , Password)  OUTPUT INSERTED.Id VALUES (@Email ,@Name ,@Password)";

              using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@Email",System.Data.SqlDbType.NVarChar,50).Value = Email;
                    cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Value = Name;
                    cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = Password;

                    int inserted = (int)cmd.ExecuteScalar();
                    user = new User()
                    {
                        Id = inserted,
                        UserName = Name,
                        Email = Email
                    };
                }

            }
            return user;
        }

        public void UpdatePassword(string Email, string Password)
        {
            int? updated;
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();

                string query = "Update Users set Password = @Password  WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar, 50).Value = Email;
                    cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = Password;

                    updated = Convert.ToInt32(cmd.ExecuteScalar());
                    
                }

            }
            if (updated == null)
            {
                throw new Exception("Cập nhật mật khẩu thất bại");
            }
            
        }
    }
}