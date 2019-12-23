using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlanIt.Startup))]
namespace PlanIt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
