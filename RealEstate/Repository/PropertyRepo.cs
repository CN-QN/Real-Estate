using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RealEstate.Repository
{
	public class PropertyRepo
	{
		private string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		 
		public List<Property> GetPropertyAll(int PageSize,int PageNumber) 
		{
			 var list = new List<Property>();

			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetPropertyPage", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.AddWithValue("@PageNumber", PageNumber);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            list.Add(MapReader(reader));

                        }
                    }
                }    
                

			}
			return list;
             
        }

        public Property MapReader(SqlDataReader reader)
        {
            return new Property
            {
                Id = (int)reader["Id"],
                UserId = (int)reader["UserId"],
                Title = (string)reader["Title"],
                Description = (string)reader["Description"],
                Price = (decimal)reader["Price"],
                Area = (decimal)reader["Area"],
                Address = (string)reader["Address"],
                City = (string)reader["City"],
                District = (string)reader["District"],
                Ward = (string)reader["Ward"],
                Status = (string)reader["Status"],

            };
        }

    }
}