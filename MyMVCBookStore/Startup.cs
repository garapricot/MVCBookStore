using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMVCBookStore.Startup))]
namespace MyMVCBookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);           
        }
    }
}
