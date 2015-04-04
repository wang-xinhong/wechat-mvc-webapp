using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WMAP.AdminWeb.Startup))]
namespace WMAP.AdminWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
