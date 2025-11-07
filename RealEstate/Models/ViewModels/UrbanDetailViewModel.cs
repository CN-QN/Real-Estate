using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class UrbanDetailViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UrbanImage> Images { get; set; }
        public List<UrbanAttribute> Attributes { get; set; }
        public Decimal Price { get; set; }
        public string Invertor { get; set; }
        public string Address  { get; set; }
    }
}