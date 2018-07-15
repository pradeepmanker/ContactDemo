using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactDemo.Startup))]
namespace ContactDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
