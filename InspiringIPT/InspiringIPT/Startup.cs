using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InspiringIPT.Startup))]
namespace InspiringIPT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
