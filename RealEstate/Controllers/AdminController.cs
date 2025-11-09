using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminService _AdminService = new AdminService();
        private readonly AgentService _AgentService = new AgentService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuanLyTin(int? PageNumber)
        {
            ViewBag.PageNumber = PageNumber ?? 1;
            return View();
        }
        public ActionResult ChiTietTin(int id)
        {
            var post = _AgentService.GetMyPostDetail(id);
            return View(post);
        }
        public ActionResult QuanLyNguoiDung(int? PageNumber)
        {
            ViewBag.PageNumber = PageNumber ?? 1;
            return View();
        }
        public ActionResult DanhSachNguoiDung(int? PageNumber)
        {
            List<DanhSachNguoiDung> DanhSachNguoiDung = _AdminService.DanhSachNguoiDung(PageNumber);
            ViewBag.Count = Math.Ceiling(Convert.ToDouble(DanhSachNguoiDung[0].TongSoNguoiDung / 30));
            ViewBag.PageNumber = PageNumber;
            ViewBag.Mode = "QuanLyNguoiDung";
            return PartialView("_DanhSachNguoiDung", DanhSachNguoiDung); 
        }

        public ActionResult TangTruong()
        {
            TangTruongViewModel TangTruong = _AdminService.TangTruong();
            return PartialView("_TangTruong", TangTruong);
        }

        public ActionResult TinDangGanDay(int? Take, int? PageNumber)
        {
            List<TinDangGanDay> RecentPosts = _AdminService.TinDangGanDay(Take, PageNumber);
            if(!Take.HasValue)
            {
                ViewBag.Count = Math.Ceiling(Convert.ToDouble(RecentPosts[0].TongSoPost / 30));
                ViewBag.PageNumber = PageNumber;
                ViewBag.Mode = "QuanLyTin";

            }
            else
            {
                ViewBag.XemThem = "/Admin/QuanLyTin?PageNumber=1";
            }
                return PartialView("_TinDangGanDay", RecentPosts);
        }

        public JsonResult TinDangMoiThang()
        {
            var data = _AdminService.TinDangMoiThang();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TiLeDangTin()
        {
            var data = _AdminService.TiLeDangTin();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            bool result = _AdminService.XoaTin(id);
            TempData["Message"] = result
                ? "Đã xóa tin thành công!"
                : "Không thể xóa tin này!";

            return RedirectToAction("QuanLyTin");
        }

        [HttpGet]
        public ActionResult EditPost(int id)
        {
            var post = _AdminService.FindPostById(id);
            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(TinDangGanDay model)
        {
            if (ModelState.IsValid)
            {
                bool result = _AdminService.EditPost(model);
                if (result)
                {
                    TempData["Message"] = "Update successful!";
                    return RedirectToAction("QuanLyTin","Admin");
                }
            }

            TempData["Error"] = "Update failed!";
            return View(model);
        }

        
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var user = _AdminService.FindUserById(id);
            if (user == null)
                return HttpNotFound();

            ViewBag.Roles = _AdminService.GetAllRoles();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(DanhSachNguoiDung model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = _AdminService.GetAllRoles();
                return View(model);
            }

            bool result = _AdminService.EditUser(model);
            TempData["Message"] = result
                ? "Cập nhật người dùng thành công!"
                : "Không thể cập nhật người dùng!";

            return RedirectToAction("QuanLyNguoiDung");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            bool result = _AdminService.DeleteUser(id);
            TempData["Message"] = result
                ? "Đã xóa người dùng thành công!"
                : "Không thể xóa người dùng này!";

            return RedirectToAction("QuanLyNguoiDung");
        }
    }
}
