using RealEstate.Models;
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
        public List<Property> GetPropertyAll(int PageSize , int PageNumber)
        {
            return _repo.GetPropertyAll(PageSize, PageNumber);
        }
    }
}