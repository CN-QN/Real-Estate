using RealEstate.Models.ViewModels;
using RealEstate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly PropertyService _propertyService = new PropertyService();
        private readonly UrbanService _urbanService = new UrbanService();
        public ActionResult Index()
        {
             

            var viewModel = new HomePageViewModel
            {
                PropetyView = _propertyService.GetPropertyAll(1),
                UrbanView= _urbanService.GetUrbanAll(),
            };

            return View(viewModel);
        }
    }
}