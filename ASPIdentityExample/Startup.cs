using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPIdentityExample.Startup))]
namespace ASPIdentityExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
