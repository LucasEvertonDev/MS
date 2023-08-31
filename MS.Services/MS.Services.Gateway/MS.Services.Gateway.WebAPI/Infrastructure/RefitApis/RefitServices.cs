using Microsoft.Extensions.DependencyInjection.Extensions;
using MS.Services.Gateway.Plugins.Redit;
using MS.Services.Gateway.Plugins.Redit.CoursesApi;
using Refit;

namespace MS.Services.Gateway.WebAPI.Infrastructure.RefitApis;

public static class RefitServices
{
    public static void AddApis(this IServiceCollection services, IConfiguration configuration)
    {

        services.TryAddTransient<SomeHandler>();

        services.AddRefitClient<ICoursesApi>().ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration["GatewayUrls:WebApiCourse"])).AddHttpMessageHandler<SomeHandler>();
        services.AddRefitClient<IStudentsApi>().ConfigureHttpClient(client => client.BaseAddress = new Uri(configuration["GatewayUrls:WebApiStudents"]));
    }
}
public class SomeHandler : DelegatingHandler
{

    public SomeHandler()
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // this resolves the issue!! WHY?
        if (request.Content != null)
        {
            var requestString = await request.Content.ReadAsStringAsync();
        }
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        response.StatusCode = System.Net.HttpStatusCode.OK;

        return response;
    }
}