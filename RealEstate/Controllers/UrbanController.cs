using RealEstate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class UrbanController : Controller
    {
        private static UrbanService _UrbanService = new UrbanService();
        public ActionResult Index()
        {
            var data = _UrbanService.GetUrbanAll();
            return View(data);
        }

        public ActionResult Details(int? id)
        {
            if(!id.HasValue)
            {
                return RedirectToAction("Index", "Urban");
            }
            var data = _UrbanService.GetUrbanDetails(id.Value);

            return View(data);

        }
    }
}