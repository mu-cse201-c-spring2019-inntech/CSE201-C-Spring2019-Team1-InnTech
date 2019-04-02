using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetTest.Startup))]
namespace NetTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
