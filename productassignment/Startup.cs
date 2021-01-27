using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(productassignment.Startup))]
namespace productassignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
