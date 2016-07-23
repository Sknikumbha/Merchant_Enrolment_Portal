using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MerchantEnrolmentPortal.Startup))]
namespace MerchantEnrolmentPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
