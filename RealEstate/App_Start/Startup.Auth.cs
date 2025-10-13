


using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin; // Đảm bảo đã import namespace này

namespace RealEstate // Tên project của bạn
{
    public partial class Startup // DÒNG NÀY PHẢI ĐƯỢC THÊM partial
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // BƯỚC 1: Cấu hình Cookie Authentication (PHẢI LÀ ĐẦU TIÊN)
            // Nó thiết lập DefaultAuthenticationTypes.ApplicationCookie
            // là SignInAsAuthenticationType mặc định.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            // BƯỚC 2: Cấu hình External Cookie (Cần cho xác thực bên ngoài)
            // Middleware này lưu tạm thời thông tin người dùng từ Google
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            // BƯỚC 3: Cấu hình Google Authentication (Sau khi có Cookie)
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "80005732805-r6vrf0gumecaieth61iiualtuja9htef.apps.googleusercontent.com", // Lấy từ Google Console
                ClientSecret = "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" // Lấy từ Google Console
                // Thuộc tính này được tự động sử dụng:
                // SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie
                // Nhưng nó cần DefaultAuthenticationTypes.ApplicationCookie đã được set ở trên.
            });
        }
    }
}