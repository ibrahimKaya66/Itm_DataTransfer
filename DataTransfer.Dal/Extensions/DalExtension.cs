using DataTransfer.Dal.Repositories.Abstract;
using DataTransfer.Dal.Repositories.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataTransfer.Dal.Extensions
{
    public static class DalExtension
    {
        public static IServiceCollection LoadDalLayerExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepositoryItm<>), typeof(RepositoryItm<>));
            return services;
        }
    }
}
