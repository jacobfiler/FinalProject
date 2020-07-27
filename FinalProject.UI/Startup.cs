using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalProject.UI.Startup))]
namespace FinalProject.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
