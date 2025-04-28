using Microsoft.Extensions.Options;
using Resyaku.Infrastructure.AppOptions;

namespace Resyaku.Web.SetupOptions
{
    public class DefaultAdminOptionsSetup(IConfiguration configuration) 
        : IConfigureOptions<DefaultAdminOptions>
    {
        private const string SectionName = "DefaultAdmin";

        public void Configure(DefaultAdminOptions options)
        {
            configuration
                .GetSection("DefaultAppData")
                .GetSection(SectionName)
                .Bind(options);
        }
    }
}
