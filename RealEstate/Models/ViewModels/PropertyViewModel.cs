using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Area { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
        
        public string ImageUrl { get; set; }
        public string Avatar { get; set; }




    }
}