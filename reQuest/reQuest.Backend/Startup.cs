using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(reQuest.Backend.Startup))]

namespace reQuest.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}