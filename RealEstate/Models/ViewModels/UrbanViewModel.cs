using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class UrbanViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public string Title { get; set; }
        public string Address{ get; set; }
        public Decimal Price { get; set; }
        public int Count { get; set; }

        public int RowsPerSlide { get; set; }
        public string CarouselId {  get; set; }
    }
}