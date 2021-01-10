using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeAPI.Startup))]
namespace TimeAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
