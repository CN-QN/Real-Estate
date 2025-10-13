using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Services;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
namespace RealEstate.Controllers
{
    public class AccountController : Controller
    {
        private UserService _UserService = new UserService();
        private AuthService _AuthService = new AuthService();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }

                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }

            private const string XsrfKey = "XsrfId";
        }
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await HttpContext.GetOwinContext().Authentication.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login"); // Chuyển về trang login nếu không lấy được info
            }

          
            var externalIdentity = loginInfo.ExternalIdentity;
            if (externalIdentity != null)
            {
                // Đăng nhập người dùng bằng External Cookie
                HttpContext.GetOwinContext().Authentication.SignIn(
                    new AuthenticationProperties { IsPersistent = false },
                    externalIdentity);

                return RedirectToLocal(returnUrl); // Chuyển hướng về trang gốc
            }

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;

            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        }
        [HttpGet]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Property"); 
        }
     
        public ActionResult Login() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModels model) 
        { 
            try
            {
                if(ModelState.IsValid)
                {
                    var user = _UserService.VerifyLogin(model.Email, model.Password);
                    if ( user!=null)
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                           new Claim(ClaimTypes.Name , user.Email),
                           new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                       },DefaultAuthenticationTypes.ApplicationCookie);
                        HttpContext.GetOwinContext().Authentication.SignIn(
                            new AuthenticationProperties { IsPersistent = true},
                            identity
                        );
                        return RedirectToAction("Index", "Property");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                        return View(model);
                    }
                }    
            }
            catch
            {
                ModelState.AddModelError("", "Đã có lỗi xảy ra. Vui lòng thử lại.");
                
            }
            return View(model);
        }

        public ActionResult Register() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string Name, string Email , String Password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_UserService.FindEmail(Email) > 0)
                    {
                        ModelState.AddModelError("Email", "Email này đã tồn tại ");
                        return View();
                    }
                    _UserService.CreateUser(Email, Name, Password);
                    return RedirectToAction("Login", "Account");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Đã có lỗi xảy ra. Vui lòng thử lại!");

            }
            return View();

        }

        public ActionResult ForgotPassword (ForgotPasswordViewModel model)
        {
            try
            {
                var user = _UserService.FindEmail(model.Email);
                if(user == -1)
                {
                    return View(model);
                }
               
                byte[] bytes = new byte[64];
                using(var rng = RandomNumberGenerator.Create()) 
                {
                    rng.GetBytes(bytes);
                }
                var token = HttpServerUtility.UrlTokenEncode(bytes);
                ResetPassword resetPassword = new ResetPassword()
                {
                    UserId = user,
                    Token = token,
                    TokenExpires = DateTime.Now.AddHours(1)
                };

                _AuthService.UpdateResetPassword(resetPassword);
                string resetLink = $"https://localhost:44362/Account/ResetPassword?token={token}&email={model.Email}";


                SendResetPasswordEmail.SendEmail(model.Email, resetLink);

              

                return RedirectToAction("SuccessResetPassword");


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return View();
        }
       
        [HttpGet]
        public ActionResult ResetPassword ([QueryString]string token , string email)
        {
         

            try
            {
                _AuthService.VerifyTokenPassword(token, email);
                return View();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Login");
                //return RedirectToAction("ResetPassword");

            }

        }
        [HttpPost]
       
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                _UserService.UpdatePassword(model.Email, model.NewPassword);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
        public ActionResult SuccessResetPassword ()
        {
            return View();
        }

    }
}