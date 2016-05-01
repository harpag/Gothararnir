using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mooshak2_Hopur5.Startup))]
namespace Mooshak2_Hopur5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
