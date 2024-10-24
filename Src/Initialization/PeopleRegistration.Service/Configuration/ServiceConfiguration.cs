using Application;
using Infrastructure;
using Infrastructure.AutoMapper;

namespace PeopleRegistration.Service.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(InfrastrutureProfile));

        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services
                .Application()
                .Infrastructure();

            return services;
        }
    }
}
