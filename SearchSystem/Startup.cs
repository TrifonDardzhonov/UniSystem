using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchSystem.Startup))]
namespace SearchSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
