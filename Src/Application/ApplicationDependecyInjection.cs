using Application.Interfaces.Application;
using Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependecyInjection
    {
        public static IServiceCollection Application(this IServiceCollection services)
        {
            services.AddScoped<IPeopleService, PeopleService>();

            return services;
        }
    }
}