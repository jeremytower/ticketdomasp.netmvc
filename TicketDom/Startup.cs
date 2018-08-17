using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketDom.Startup))]
namespace TicketDom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
