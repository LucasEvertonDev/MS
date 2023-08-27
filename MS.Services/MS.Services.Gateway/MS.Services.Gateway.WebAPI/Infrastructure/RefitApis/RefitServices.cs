using MS.Services.Gateway.Plugins.Redit.CoursesApi;
using Refit;

namespace MS.Services.Gateway.WebAPI.Infrastructure.RefitApis;

public static class RefitServices
{
    public static void AddApis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<ICoursesApi>().ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7049"));
    }
}
