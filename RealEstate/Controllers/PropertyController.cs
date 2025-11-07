using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace RealEstate.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property

        PropertyService _service = new PropertyService();
        


        public ActionResult Index(string keyword)
        {
            if (keyword == null)
            {
                return RedirectToAction("Index","Home"); 
            }
            var property = _service.GetPropertySearch(keyword);

            return View(property);
        }
        
        public ActionResult Details(int? id)
        {

         
            if(id.HasValue)
            {
                var property = _service.GetPropertyById(id.Value);
                return View(property);

            }
            return RedirectToAction("Index", "Home");

        }
       
        // GET: Property/Create
        public ActionResult RelatedProperty(int? Id , int? TypeId)
        {

            if (TypeId.HasValue)
            {
                var property = _service.GetRelatedProperty(Id.Value, TypeId.Value);
                return PartialView("_ListProperty", property);

            }
            return  HttpNotFound();

        }


    }
}
