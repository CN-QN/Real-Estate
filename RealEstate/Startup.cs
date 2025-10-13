// File Startup.cs
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealEstate.Startup))]
namespace RealEstate
{
    public partial class Startup // Quan trọng phải là partial
    {
        public void Configuration(IAppBuilder app)
        {
            // Lời gọi này sẽ hoạt động sau khi bạn định nghĩa ConfigureAuth trong Startup.Auth.cs
            ConfigureAuth(app);
        }
    }
}