using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Guoke.Web.Startup))]
namespace Guoke.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
