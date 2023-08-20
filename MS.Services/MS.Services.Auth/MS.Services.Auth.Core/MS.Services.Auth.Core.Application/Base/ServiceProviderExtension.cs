using MS.Libs.Core.Domain.DbContexts.UnitOfWork;

namespace MS.Services.Auth.Core.Application.Base
{
    public static class ServiceProviderExtensions
    {
        public static TInstance GetService<TInstance>(this IServiceProvider serviceProvider)
        {
            return (TInstance)serviceProvider.GetService(typeof(TInstance));
        }
    }
}
