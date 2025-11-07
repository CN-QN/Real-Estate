using RealEstate.Models;
using RealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Repository
{
    public class UrbanRepo
    {
        RealEstateEntities db = new RealEstateEntities();

        
        public List<UrbanViewModel> GetUrbanAll()
        {
            List<UrbanViewModel> data = db.Urbans.Select(x => new UrbanViewModel()
            {
                Id = x.Id,
                Url = x.UrbanImages.Select(img =>img.UrlImage).FirstOrDefault(),
                Price=x.Price.Value,
                Title = x.Title,
                Address = x.Address_Details

            }).ToList();

            return data;
        }
        public UrbanDetailViewModel GetUrbanDetails(int id)
        {
            var data = db.Urbans.Where(x => x.Id == id).Select(x => new UrbanDetailViewModel()
            {
                Title = x.Title,
                
                Description = x.Description,
                Images = x.UrbanImages.ToList(),
                Attributes = x.UrbanAttributes.ToList(),
                Price = x.Price.Value,
                Invertor = x.Investor,
                Address = x.Address_Details

            }).FirstOrDefault();
            return data;
        }
    }
}