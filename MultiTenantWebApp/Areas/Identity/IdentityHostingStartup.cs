using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MultiTenantWebApp.Areas.Identity.IdentityHostingStartup))]
namespace MultiTenantWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}