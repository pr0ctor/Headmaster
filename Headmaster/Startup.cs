using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Headmaster.Startup))]
namespace Headmaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
