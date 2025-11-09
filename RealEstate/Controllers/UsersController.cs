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
        private AuthService _AuthService = new AuthService();

        [Authorize]
        public ActionResult ProfileUser()
        {
            try
            {
                var email = User.Identity.Name;
                var user = _UserService.GetProfile(email);
                if (user == null) return RedirectToAction("Login", "Account");
 

                return View(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileUser(UserProfileVIewModel model)
        {
            try
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var id =Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var email = User.Identity.Name;
                var user = _AuthService.FindEmail(email);
                if (user == null) return RedirectToAction("Login");
                _UserService.UpdateFullProfile(id,model);

                // Cập nhật tên
                if (!string.IsNullOrEmpty(model.Name))
                    _UserService.UpdateName(email, model.Name);

                // Nếu không phải tài khoản Google thì cho đổi mật khẩu
                if (!user.ProviderName?.Equals("Google", StringComparison.OrdinalIgnoreCase) == true)
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