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
using System.Web.Helpers;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
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


        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            try
            {
                var loginInfo = await HttpContext.GetOwinContext().Authentication.GetExternalLoginInfoAsync();
                if (loginInfo == null)
                {
                    return RedirectToAction("Login");
                }

                var providerName = loginInfo.Login.LoginProvider;
                var providerKey = loginInfo.Login.ProviderKey;
                var Email = loginInfo.Email;
                var Name = loginInfo.ExternalIdentity?.Name;


                var user = _UserService.FindEmail(Email);
                if (user != null && user.ProviderName != "Google")
                {
                    if (_UserService.FindEmail(Email) == null)
                    {
                        ModelState.AddModelError("Email", "Email này đã đăng ký tài khoản khác !");
                        return View();
                    }
                }
                if (user == null)
                {
                    _UserService.CreateUser(Email, Name, null, providerName, providerKey);
                    user = _UserService.FindEmail(Email);



                }
                else
                {
                    _UserService.UpdateProvider(Email, providerName, providerKey);
                    //ViewBag.ReturnUrl = returnUrl;
                    //ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                }
                var identity = new ClaimsIdentity(new[]
                     {
                      new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                  }, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(
                    new AuthenticationProperties { IsPersistent = true },
                    identity
                    );
                TempData["ToastrType"] = "success";
                TempData["ToastrMessage"] = "Đăng nhập thành công ";
                return RedirectToAction("Index", "Property");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToLocal(returnUrl);


        }
        [HttpGet]

        [AllowAnonymous]

        [RedirectAuthenticated]

        public ActionResult Login() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        [RedirectAuthenticated]

        public ActionResult Login(LoginViewModels model) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _UserService.VerifyLogin(model.Email, model.Password);
                    if (user != null)
                    {
                        var identity = new ClaimsIdentity(new[]
                        {
                           new Claim(ClaimTypes.Name , user.Email),
                           new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                       }, DefaultAuthenticationTypes.ApplicationCookie);

                        HttpContext.GetOwinContext().Authentication.SignIn(
                            new AuthenticationProperties { IsPersistent = true },
                            identity
                        );
                        TempData["ToastrType"] = "success";
                        TempData["ToastrMessage"] = "Đăng nhập thành công ";
                        return RedirectToAction("Index", "Property");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            return View(model);
        }

        [RedirectAuthenticated]
        [AllowAnonymous]

        public ActionResult Register() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        [RedirectAuthenticated]
        public ActionResult Register(RegisterViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_UserService.FindEmail(model.Email) ==null)
                    {
                        _UserService.CreateUser(model.Email, model.Name, model.Password, null, null);
                        return RedirectToAction("Login", "Account");
                    
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Email đã tòn tại");
                        return View();
                    }
                

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }
            return View();

        }

        public ActionResult ForgotPassword (ForgotPasswordViewModel model)
        {
            try
            {
                var user = _UserService.FindEmail(model.Email);

                if (user == null || user.ProviderName == "Google")
                {
                    ModelState.AddModelError("", "Email này chưa tồn tại");
                    return View();
                }
                byte[] bytes = new byte[64];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }
                var token = HttpServerUtility.UrlTokenEncode(bytes);
                ResetPassword resetPassword = new ResetPassword()
                {
                    User_id = user.Id,
                    Token = token,
                    Expires = DateTime.Now.AddHours(1)
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Login");

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

        [AllowAnonymous]

        public ActionResult Logout()
        {
            TempData["ToastrType"] = "success";
            TempData["ToastrMessage"] = "Đăng xuất thành công";
            TempData.Keep();

            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Property");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)
                && Url.IsLocalUrl(returnUrl)
                && !returnUrl.Contains("/Account/Login"))
            {
                return Redirect(returnUrl);
            }
          
            return RedirectToAction("Index", "Property");
        }

    }
}