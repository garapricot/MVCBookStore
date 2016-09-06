using DAL.Context;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;

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
