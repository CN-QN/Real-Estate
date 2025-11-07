using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class ListBaseViewModel
    {
        public string Title { get; set; } // tiêu đề section, ví dụ: "Dự án nổi bật"
        public string CarouselId { get; set; } // để phân biệt mỗi carousel
        public int RowsPerSlide { get; set; } = 1;
        public List<PropertyViewModel> Properties { get; set; } = new List<PropertyViewModel>();
        public List<UrbanViewModel> Urbans { get; set; } =  new List<UrbanViewModel>();
    }
}