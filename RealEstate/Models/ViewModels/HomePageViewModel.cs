using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class HomePageViewModel
    {
        public List<PropertyViewModel> PropetyView { get; set; }
        public List<UrbanViewModel> UrbanView { get; set; }
    }
}