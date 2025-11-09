using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace RealEstate.Common
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public AuthorizeCustomAttribute(params string[] roles)
        {
            this.allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User;

            // Chưa login
            if (!user.Identity.IsAuthenticated)
                return false;

            // Nếu không truyền role nào -> chỉ cần login là đủ
            if (allowedRoles == null || allowedRoles.Length == 0)
                return true;

            // Kiểm tra role
            var claimsIdentity = user.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return false;

            var roleClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null)
                return false;

            return allowedRoles.Contains(roleClaim.Value, StringComparer.OrdinalIgnoreCase);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Nếu chưa login -> chuyển tới Login
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
            else
            {
                // Nếu đã login nhưng không đủ quyền -> báo lỗi hoặc redirect khác
                filterContext.Result = new RedirectResult("/Error/AccessDenied");
            }
        }
    }
}
