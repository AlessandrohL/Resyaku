using Microsoft.Extensions.DependencyInjection;

namespace Resyaku.Application
{
    public static class DepedencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
