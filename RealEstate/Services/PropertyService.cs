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
        public List<PropertyViewModel> GetPropertyAll(  int PageNumber)
        {
            return _repo.GetPropertyAll(  PageNumber);
        }
        public List<PropertyDetailViewModel> GetPropertySearch(string Keyword)
        {
            return _repo.GetPropertySearch(Keyword);
        }
        
        public  PropertyDetailViewModel GetPropertyById(int Id) 
        {
            return _repo.GetPropertyById(Id);

        }
        public List<PropertyViewModel> GetRelatedProperty(int Id,int Id_Type)
        {
            return _repo.GetRelatedProperty(Id, Id_Type);

        }
        public List<PropertyType> GetPropertyTypes()
        {
            return _repo.GetPropertyTypes();

        }

    }
}