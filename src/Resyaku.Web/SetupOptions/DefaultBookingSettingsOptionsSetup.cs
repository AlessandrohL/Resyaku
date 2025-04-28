using Microsoft.Extensions.Options;
using Resyaku.Infrastructure.AppOptions;

namespace Resyaku.Web.SetupOptions
{
    public class DefaultBookingSettingsOptionsSetup(IConfiguration configuration)
        : IConfigureOptions<DefaultBookingSettingsOptions>
    {
        private const string SectionName = "DefaultBookingSettings";

        public void Configure(DefaultBookingSettingsOptions options)
        {
            configuration
                .GetSection("DefaultAppData")
                .GetSection(SectionName)
                .Bind(options);
        }
    }
}
