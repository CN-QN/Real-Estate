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
        public decimal Area { get; set; }
        public string NameUser { get; set; }
        public string NameType { get; set; }

        public int Price { get; set; }
        public string Address { get; set; }
  

        public List<ImageItem> ImageGallery { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}