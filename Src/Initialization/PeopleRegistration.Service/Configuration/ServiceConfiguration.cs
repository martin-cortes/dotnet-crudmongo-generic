using Application;
using Application.Common.Health;
using Infrastructure;
using Infrastructure.AutoMapper;

namespace PeopleRegistration.Service.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(InfrastrutureProfile));

        public static IServiceCollection RegisterHealthCheck(this IServiceCollection services) =>
                services.AddHostedService<HealthCheckHostedService>();

        public static IServiceCollection RegisterCors(this IServiceCollection services,
                                                           string policyName,
                                                           string url) => 
            services.AddCors(o =>
            {
                o.AddPolicy(policyName, builder =>
                {
                 builder.WithOrigins(url)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services
                .Application()
                .Infrastructure();

            return services;
        }
    }
}
