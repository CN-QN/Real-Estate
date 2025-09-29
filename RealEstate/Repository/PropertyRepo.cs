using Newtonsoft.Json;
using RealEstate.Models;
using RealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RealEstate.Repository
{
	public class PropertyRepo
	{
		private string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		 
		public List<PropertyViewModel> GetPropertyAll(int PageSize,int PageNumber) 
		{
			 var list = new List<PropertyViewModel>();

			using (SqlConnection conn = new SqlConnection(connStr))
			{
				conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetProperty", conn))
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
        public PropertyDetailViewModel GetPropertyById(int Id)
        {
            PropertyDetailViewModel propertyDetailViewModel = new PropertyDetailViewModel();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open ();
                using (SqlCommand cmd = new SqlCommand("GetPropertyById", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {

                            propertyDetailViewModel.Id = Convert.ToInt32(reader["Id"]);
                            propertyDetailViewModel.UserId = Convert.ToInt32(reader["UserId"]);
                            propertyDetailViewModel.Title = Convert.ToString(reader["Title"]);
                            propertyDetailViewModel.Description = Convert.ToString(reader["Description"]);
                            propertyDetailViewModel.Area = Convert.ToDecimal(reader["Area"]);
                            propertyDetailViewModel.NameType = Convert.ToString(reader["NameType"]);
                            propertyDetailViewModel.NameUser = Convert.ToString(reader["NameUser"]);

                            propertyDetailViewModel.Price = Convert.ToInt32(reader["Price"]);
                            propertyDetailViewModel.Address = Convert.ToString(reader["Area"]);
                            propertyDetailViewModel.Phone = Convert.ToString(reader["Phone"]);
                            propertyDetailViewModel.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            propertyDetailViewModel.ImageGallery = JsonConvert.DeserializeObject<List<ImageItem>>(Convert.ToString(reader["ImageGallery"]));




                        }
                    }

                }
            }
            return propertyDetailViewModel;

        }
        public PropertyViewModel MapReader(SqlDataReader reader)
        {
            return new PropertyViewModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Title =  reader["Title"].ToString(),
                Price = Convert.ToInt32(reader["Price"]),
                Area = Convert.ToDecimal(reader["Area"]),
                Address =  reader["Address"].ToString(),
                ImageUrl =  reader["ImageUrl"].ToString(),
                Name =  reader["Name"].ToString(),
                Avatar =  reader["Avatar"].ToString(),

            };
        }

    }
}