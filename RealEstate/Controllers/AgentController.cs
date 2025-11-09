using Microsoft.AspNet.Identity;
using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class AgentController : Controller
    {
        private AgentService _AgentService = new AgentService();
        private PropertyService _PropertyService = new PropertyService();


        public ActionResult Index()
        {
            ViewBag.Provinces = _AgentService.Provinces();
            ViewBag.Districts = new List<District>();
            ViewBag.Wards = new List<Ward>();
            ViewBag.Type = new SelectList(_PropertyService.GetPropertyTypes(), "Id", "Name", null); 
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

        public ActionResult MyPosts(int pageNumber = 1)
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            var posts = _AgentService.GetMyPosts(userId, pageNumber);

            if (posts == null || !posts.Any())
            {
                ViewBag.Message = "Bạn chưa đăng bài viết nào.";
            }

            return View(posts);
        }


        [HttpPost]
        [ValidateInput(false)]  
        public ActionResult CreatePost(PostViewModels request, List<HttpPostedFileBase> ImageUrls)
        {

            int currentUserId = Convert.ToInt32(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                bool result = _AgentService.CreatePost(request, ImageUrls.ToList(), currentUserId);

                if (result)
                {
                    TempData["ToastrType"] = "success";
                    TempData["ToastrMessage"] = "Đăng tin thành công. Tin của bạn đang chờ duyệt.";
                    return RedirectToAction("MyPosts", "Agent");
                }
            }


            ViewBag.Provinces = _AgentService.Provinces();
            ViewBag.Districts = new List<District>();
            ViewBag.Wards = new List<Ward>();
            ViewBag.Type = new SelectList(_PropertyService.GetPropertyTypes(), "Id", "Name", null);
            return View("Index", request);


        }
        public ActionResult MyPostDetail(int? id)
        {
            if(id ==null)
            {
                return RedirectToAction("Index", "Agent");
            }
            var post = _AgentService.GetMyPostDetail(id.Value );
            if (post == null)
            {
                TempData["ToastrType"] = "error";
                TempData["ToastrMessage"] = "Không tìm thấy bài viết.";
                return RedirectToAction("MyPosts");
            }
            return View(post);
        }


        public ActionResult TinCuaToi()
        {
            return View();
        }
        public ActionResult Customers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteImage(int id, string name)
        {
            try
            {
                using (var db = new RealEstateEntities())
                {

                    var post = db.PropertyImages.Where(p => p.PropertyId == id && p.ImageUrl == name).FirstOrDefault();
                    if (post == null)
                        return Json(new { success = false, message = "Không tìm thấy bài đăng." });

                    db.PropertyImages.Remove(post);
                    db.SaveChanges();

                    // Xóa file vật lý
                    var path = Server.MapPath("~/Content/Uploads/" + name);
                    if(System.IO.File.Exists(path))

                    {
                        System.IO.File.Delete(path);
                    }
                    return View("EditPost", "Agent",id);
                }
            }
            catch
            {
                throw new Exception("Error");
            }
        }

        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Agent");
            }
            ViewBag.Provinces = _AgentService.Provinces();
            ViewBag.Districts = new List<District>();
            ViewBag.Wards = new List<Ward>();
            ViewBag.Type = new SelectList(_PropertyService.GetPropertyTypes(), "Id", "Name", null);
            var item = _AgentService.GetMyPostDetail(id.Value );

            return View(item);
        }

        [HttpPost , ActionName("EditPost"),ValidateInput(false)]
        public ActionResult EditPosts(GetPropertyDetail_Result model , int id)
        {
            _AgentService.EditPost(model, id);
            return RedirectToAction("MyPosts","Agent");
        }

        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Agent");
            }
            var item = _AgentService.GetMyPostDetail(id.Value );
            return View();
        }
        [HttpPost, ActionName("DeletePost")]
        public ActionResult DeletePosts(int id)
        {
            var isSuccess = _AgentService.DeletePost(id);
            if(isSuccess == true)
            {
                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = "Xóa bài viết thành công.";

            }
            else
            {
                TempData["ToastrType"] = "error";
                TempData["ToastrMessage"] = "Xóa bài viết thất bại.";
            }
            return RedirectToAction("MyPosts", "Agent");

        }

    }
}
