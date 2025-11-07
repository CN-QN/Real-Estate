using RealEstate.Models;
using RealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Repository
{
    public class NewRepo
    {
        RealEstateEntities db = new RealEstateEntities();
        public List<PropertyViewModel> GetNewHot()
        {
            //db.News.Select(x => new PropertyView
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    ImageUrl = x.
            //}
            return null;
        }

        public List<PropertyViewModel> GetNewAll()
        {
            return null;
        }
    }
}