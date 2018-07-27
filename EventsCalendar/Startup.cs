using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventsCalendar.Startup))]
namespace EventsCalendar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
