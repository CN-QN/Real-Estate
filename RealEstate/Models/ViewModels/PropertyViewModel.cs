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
        public decimal AreaMin { get; set; }
        public decimal AreaMax { get; set; }
        public string AreaUnit { get; set; }

        public string Name { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        public string PriceUnit { get; set; }

        public string Address { get; set; }

        public int TotalPage { get; set; }
        public string ImageUrl { get; set; }

        public string Avatar { get; set; }




    }
}