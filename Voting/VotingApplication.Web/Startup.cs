using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using VotingApplication.Data.Context;

[assembly: OwinStartupAttribute(typeof(VotingApplication.Web.Startup))]
namespace VotingApplication.Web
{
    public partial class Startup
    {
       
            public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
