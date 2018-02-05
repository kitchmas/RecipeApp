using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipeApp.WebUI.Startup))]
namespace RecipeApp.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
