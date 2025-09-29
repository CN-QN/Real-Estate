using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace RealEstate.Services
{
    public class PropertyService
    {
        private PropertyRepo _repo = new PropertyRepo();
        public List<PropertyViewModel> GetPropertyAll(int PageSize , int PageNumber)
        {
            return _repo.GetPropertyAll(PageSize, PageNumber);
        }

        public  PropertyDetailViewModel GetPropertyById   (int Id) 
        {
            return _repo.GetPropertyById(Id);

        }
    }
}