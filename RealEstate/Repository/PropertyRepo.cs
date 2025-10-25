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
        RealEstateEntities db = new RealEstateEntities();

		 
		public List<PropertyViewModel> GetPropertyAll( int PageNumber, int PageSize = 40 ) 
		{
              
            List<PropertyViewModel> ProperyList = db.GetProperty(PageSize, PageNumber)
                 .Select(o => new PropertyViewModel
                 {
                     Id = o.Id,
                     Title = o.Title,
                     AreaMin = o.AreaMin ?? 0,
                     AreaMax = o.AreaMax ?? 0,
                     AreaUnit = o.AreaUnit,
                     Name = o.Name ?? "",
                     PriceMin = o.PriceMin ?? 0,
                     PriceMax = o.PriceMax ?? 0,
                     PriceUnit = o.PriceUnit,

                     Address = o.Address,

                     TotalPage = Convert.ToInt32(o.TotalPage),

                     ImageUrl = Convert.ToString(o.ImageUrl),

                     Avatar = o.Avatar ?? ""
                 }).ToList();


             return ProperyList;
        }
        public PropertyDetailViewModel GetPropertyById(int Id)
        {
            PropertyDetailViewModel propertyDetailViewModel = db.GetPropertyById(Id).Select(i =>new PropertyDetailViewModel
            {
                Id = i.Id,
                UserId = i.UserId,
                Title = i.Title,
                Description = i.Description,
                AreaMax = Convert.ToDecimal(i.AreaMax),
                AreaMin = Convert.ToDecimal(i.AreaMin),
                AreaUnit = Convert.ToString(i.AreaUnit),
                NameType = i.NameType,
                NameUser = i.NameUser,
                Avatar  = i.Avatar ?? "",
                PriceMax = Convert.ToInt32(i.PriceMax),
                PriceMin = Convert.ToInt32(i.PriceMin),
                PriceUnit = Convert.ToString(i.PriceUnit),
                Address = i.Address ?? "",
                Phone = i.Phone ?? "",
                CreatedAt = Convert.ToDateTime(i.CreatedAt),
                ImageGallery = JsonConvert.DeserializeObject<List<PropertyImage>>(Convert.ToString(i.ImageGallery)),


            }).FirstOrDefault();
          

             
            return propertyDetailViewModel;

        }
        

    }
}