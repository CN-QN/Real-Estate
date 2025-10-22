


using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin;  

namespace RealEstate  
{
    public partial class Startup  
    {
        public void ConfigureAuth(IAppBuilder app)
        {
             
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
             
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "80005732805-r6vrf0gumecaieth61iiualtuja9htef.apps.googleusercontent.com",  
                ClientSecret = "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" ,

        
            });
        }
    }
}