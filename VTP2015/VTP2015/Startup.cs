using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VTP2015.Startup))]
namespace VTP2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
