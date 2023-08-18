using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MS.Libs.Infra.IoC
{
    public abstract class BaseDependencyInjection
    {
        public abstract void AddInfraSctructure(IServiceCollection services, IConfiguration configuration);

        protected abstract void AddDbContexts(IServiceCollection services, IConfiguration configuration);

        protected abstract void AddServices(IServiceCollection services, IConfiguration configuration);

        protected abstract void AddValidators(IServiceCollection services, IConfiguration configuration);

        protected abstract void AddRepositorys(IServiceCollection services, IConfiguration configuration);

        protected abstract void AddMappers(IServiceCollection services, IConfiguration configuration);
    }
}
