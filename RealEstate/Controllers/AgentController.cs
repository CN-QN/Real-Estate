using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class AgentController : Controller
    {
        private readonly AgentService _AgentService = new AgentService();
        private readonly PostService _PostService = new PostService();

        public ActionResult Index()
        {
            ViewBag.Provinces = _AgentService.Provinces()
                .Select(p => new SelectListItem { Value = p.code.ToString(), Text = p.name })
                .ToList();
            ViewBag.Districts = new List<SelectListItem>();
            ViewBag.Wards = new List<SelectListItem>();
            return View();
        }

        [HttpGet]
        public JsonResult Districts(int provinceCode)
        {
            var districts = _AgentService.Districts(provinceCode)
                .Select(d => new { d.code, d.name })
                .ToList();
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Wards(int districtCode)
        {
            var wards = _AgentService.Wards(districtCode)
                .Select(w => new { w.code, w.name })
                .ToList();
            return Json(wards, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyPosts()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThemMoiDanhSach(PostFormVM model)
        {
            ViewBag.Provinces = _AgentService.Provinces()
                .Select(p => new SelectListItem { Value = p.code.ToString(), Text = p.name })
                .ToList();
            ViewBag.Districts = string.IsNullOrEmpty(model?.ProvinceCode)
                ? new List<SelectListItem>()
                : _AgentService.Districts(int.Parse(model.ProvinceCode))
                    .Select(d => new SelectListItem { Value = d.code.ToString(), Text = d.name })
                    .ToList();
            ViewBag.Wards = string.IsNullOrEmpty(model?.DistrictCode)
                ? new List<SelectListItem>()
                : _AgentService.Wards(int.Parse(model.DistrictCode))
                    .Select(w => new SelectListItem { Value = w.code.ToString(), Text = w.name })
                    .ToList();

            var hasAnyFile = model?.ImageFiles != null &&
                             model.ImageFiles.Any(f => f != null && f.ContentLength > 0);
            if (!hasAnyFile)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa!";
                return View("Index", model);
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                var ok = _PostService.CreatePost(model);
                if (ok) return RedirectToAction("MyPosts");
                ModelState.AddModelError("", "Không thể lưu tin đăng vào cơ sở dữ liệu.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
            }

            return View("Index", model);
        }
    }
}
