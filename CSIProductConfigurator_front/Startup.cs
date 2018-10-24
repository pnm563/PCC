using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSIProductConfigurator_front.Startup))]
namespace CSIProductConfigurator_front
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
