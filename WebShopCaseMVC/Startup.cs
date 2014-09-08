using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebShopCaseMVC.Startup))]
namespace WebShopCaseMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
