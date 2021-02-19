using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(transitProject.Startup))]
namespace transitProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
