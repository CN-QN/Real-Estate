using System;
using System.Web;

namespace RealEstate.Models
{
    public class AuthModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += (sender, e) =>
            {
                var app = (HttpApplication)sender;
                var request = app.Context.Request;
                var response = app.Context.Response;
                var user = app.Context.User;

                if (request.Url.AbsolutePath.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
                {

                    if (user == null || !user.Identity.IsAuthenticated)
                    {
                        response.Redirect("/Account/Login");
                    }
                    else if (!user.IsInRole("Admin"))
                    {
                        response.Redirect("/Home");
                    }
                }
            };

        }
        public void Dispose() { }
    }
}