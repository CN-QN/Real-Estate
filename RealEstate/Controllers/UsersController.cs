using RealEstate.Models.ViewModels;
using RealEstate.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        private UserService _UserService = new UserService();


        [Authorize]
        public ActionResult ProfileUser()
        {
            try
            {
                var email = User.Identity.Name;
                var user = _UserService.FindEmail(email);
                if (user == null) return RedirectToAction("Login");

                var model = new ProfileViewModel
                { 
                   Id = user.UserId,
                   Bio = user.Bio,
                   Email = user.User.Email,
                   Address = user.Address,
                   Website = user.Website,
                   Facebook = user.Facebook,
                   Instagram = user.Instagram,
                   Name = user.User.Name,
                   Gender = user.Gender,
                   CoverPhoto = user.CoverPhoto,
                   DateOfBirth = user.DateOfBirth,
                   ProviderName = user.User.ProviderName
                   
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUser(ProfileViewModel model)
        {
            try
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var id =Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var email = User.Identity.Name;
                var user = _UserService.FindEmail(email);
                if (user == null) return RedirectToAction("Login");
                _UserService.UpdateFullProfile(id,model);

                // Cập nhật tên
                if (!string.IsNullOrEmpty(model.Name))
                    _UserService.UpdateName(email, model.Name);

                // Nếu không phải tài khoản Google thì cho đổi mật khẩu
                if (!user.User.ProviderName?.Equals("Google", StringComparison.OrdinalIgnoreCase) == true)
                {
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        if (model.NewPassword == model.ConfirmPassword)
                        {
                            _UserService.UpdatePassword(email, model.NewPassword);
                        }
                        else
                        {
                            ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp");
                            return View(model);
                        }
                    }
                }

                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = "Cập nhật thông tin thành công!";
                return RedirectToAction("ProfileUser");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

    }
}