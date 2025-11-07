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
using RealEstate.Models.ViewModels;
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
                 
                return Json(districts,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Wards( int districtCode)
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

        [HttpPost]
        [ValidateInput(false)] // Cho phép nhận HTML từ TinyMCE (Mô tả)
        public JsonResult PostNews(PostViewModels request)
        {
            // GIẢ ĐỊNH: Lấy UserID của Agent đang đăng nhập.
            // Trong thực tế, bạn cần xác thực và lấy ID từ Session/Cookie/Identity.
            int currentUserId = 1; // THAY THẾ BẰNG ID THỰC TẾ

            if (request == null)
            {
                return Json(new { success = false, message = "Dữ liệu gửi lên không hợp lệ." });
            }

            // Kiểm tra tối thiểu 3 ảnh
            if (request.ImageUrls == null || request.ImageUrls.Count < 3)
            {
                return Json(new { success = false, message = "Vui lòng tải lên ít nhất 3 ảnh." });
            }

            bool result = _AgentService.AddPost(request, currentUserId);

            if (result)
            {
                return Json(new { success = true, message = "Đăng tin thành công. Tin của bạn đang chờ duyệt." });
            }
            else
            {
                // Thông báo lỗi chung, lỗi chi tiết đã được log ở Repo
                return Json(new { success = false, message = "Lỗi hệ thống khi lưu tin đăng. Vui lòng thử lại." });
            }
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
