using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MultiLC1.Startup))]
namespace MultiLC1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
