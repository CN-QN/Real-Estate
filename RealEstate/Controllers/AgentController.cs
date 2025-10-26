using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using RealEstate.Models;
using RealEstate.Services;

namespace RealEstate.Controllers
{
    public class AgentController : Controller
    {
        private AgentService _AgentService = new AgentService();

        public ActionResult Index()
        {
            ViewBag.Provinces = _AgentService.Provinces();
            ViewBag.Districts = new List<District>();
            ViewBag.Wards = new List<Ward>();
            return View();
        }
        [HttpGet]
        public JsonResult Districts(int provinceCode)
        {
            try
            {
                var districts = _AgentService.Districts(provinceCode)
                    .Select(d => new { d.code, d.name })
                .ToList();

                return Json(districts, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Wards(int districtCode)
        {
            var wards = _AgentService.Wards(districtCode)
        .Select(w => new { w.code, w.name })
        .ToList();
            return Json(wards, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult MyPosts()
        {
            return View();
        }
        public ActionResult TinCuaToi()
        {
            return View();
        }
        public ActionResult Customers()
        {
            return View();
        }
    }
}