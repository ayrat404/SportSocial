using Microsoft.Owin;
using Owin;
using Social;

[assembly: OwinStartup(typeof(Startup))]
namespace Social
{

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}