using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class PropertyListViewModel
    {
        public List<PropertyViewModel> Properties { get; set; }
        public int RowsPerSlide { get; set; }
        public string CarouselId { get; set; }   // 👈 thêm dòng này
    }
}