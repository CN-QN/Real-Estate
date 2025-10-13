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
using RealEstate.Utils;
namespace RealEstate.Repository
{
	public class PropertyRepo
	{
        private SqlConnection connStr = DbHelper.GetConnection();
		 
		public List<PropertyViewModel> GetPropertyAll(int PageSize,int PageNumber) 
		{
			 var list = new List<PropertyViewModel>();

			using (SqlConnection conn = connStr)
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
            using (SqlConnection conn =  connStr)
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
                            propertyDetailViewModel.AreaUnit = Convert.ToString(reader["AreaUnit"]);
                            propertyDetailViewModel.AreaMax = Convert.ToDecimal(reader["AreaMax"]);
                            propertyDetailViewModel.AreaMin = Convert.ToDecimal(reader["AreaMin"]);

                            propertyDetailViewModel.NameType = Convert.ToString(reader["NameType"]);
                            propertyDetailViewModel.NameUser = Convert.ToString(reader["NameUser"]);
                            propertyDetailViewModel.Avatar = Convert.ToString(reader["Avatar"]);


                            propertyDetailViewModel.PriceMin = Convert.ToDecimal(reader["PriceMin"]);
                            propertyDetailViewModel.PriceMax = Convert.ToDecimal(reader["PriceMax"]);
                            propertyDetailViewModel.PriceUnit = Convert.ToString(reader["PriceUnit"]);

                            propertyDetailViewModel.Address = Convert.ToString(reader["Address"]);
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
                PriceMin = Convert.ToDecimal(reader["PriceMin"]),
                PriceMax = Convert.ToDecimal(reader["PriceMax"]),
                PriceUnit = Convert.ToString(reader["PriceUnit"]),

                AreaMin = Convert.ToDecimal(reader["AreaMin"]),
                AreaMax = Convert.ToDecimal(reader["AreaMax"]),
                AreaUnit = Convert.ToString(reader["AreaUnit"]),

                Address =  reader["Address"].ToString(),
                ImageUrl =  JsonConvert.DeserializeObject<List<ImageItem>>(Convert.ToString(reader["ImageUrl"].ToString())),
                Name =  reader["Name"].ToString(),
                Avatar =  reader["Avatar"].ToString(),

            };
        }

    }
}