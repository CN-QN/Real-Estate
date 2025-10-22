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
        public ActionResult Index( int id  =1)
        {
             List<PropertyViewModel> property = _service.GetPropertyAll(id);
             ViewBag.PageSize = id;
            return View(property);
        }

        // GET: Property/Details/5
        public ActionResult Details(int? id)
        {

         
            if(id.HasValue)
            {
                var property = _service.GetPropertyById(id.Value);
                return View(property);

            }

            return null;
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Property/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Property/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Property/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Property/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Property/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
