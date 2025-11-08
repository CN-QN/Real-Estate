using Newtonsoft.Json;
using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
namespace RealEstate.Repository
{
	public class PropertyRepo
	{
        RealEstateEntities db = new RealEstateEntities();

		 
		public List<PropertyViewModel> GetPropertyAll( int PageNumber ) 
		{
              
            List<PropertyViewModel> ProperyList = db.GetProperty(  PageNumber)
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

        public List<PropertyType> GetPropertyTypes()
        {
            return db.PropertyTypes.ToList();
        }
        public List<PropertyDetailViewModel> GetPropertySearch(string Keyword)
        {

            List<PropertyDetailViewModel> propertyDetailViewModel = db.GetPropertySearch(Keyword).Select(i => new PropertyDetailViewModel
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
                TypeId = i.TypeId,
                Avatar = i.Avatar ?? "",
                PriceMax = Convert.ToInt32(i.PriceMax),
                PriceMin = Convert.ToInt32(i.PriceMin),
                PriceUnit = Convert.ToString(i.PriceUnit),
                Address = i.Address ?? "",
                Phone = i.Phone ?? "",
                CreatedAt = Convert.ToDateTime(i.CreatedAt),
                ImageGallery = JsonConvert.DeserializeObject<List<PropertyImage>>(Convert.ToString(i.ImageGallery)),


            }).ToList();



            return propertyDetailViewModel;
        }

        
        public List<PropertyViewModel> GetRelatedProperty(int Id ,int Id_Type)
        {

            int PageNumber = 1;
            List<PropertyViewModel> ProperyList = db.GetProperty( PageNumber)
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
                   TypeId = o.TypeId,
                   Address = o.Address,

                   TotalPage = Convert.ToInt32(o.TotalPage),

                   ImageUrl = Convert.ToString(o.ImageUrl),

                   Avatar = o.Avatar ?? ""
               })
               .Where(x => x.TypeId == Id_Type && x.Id !=Id)
               .ToList();


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
                TypeId = i.TypeId,
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