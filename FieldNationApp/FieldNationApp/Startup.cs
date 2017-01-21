using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FieldNationApp.Startup))]
namespace FieldNationApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
