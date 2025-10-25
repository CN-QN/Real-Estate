using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class PropertyDetailViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? AreaMin { get; set; }
        public decimal? AreaMax { get; set; }
        public string AreaUnit { get; set; }

        public string NameUser { get; set; }
        public string NameType { get; set; }
        public string Avatar { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        public string PriceUnit { get; set; }
        public string Address { get; set; }
  

        public List<PropertyImage> ImageGallery { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}