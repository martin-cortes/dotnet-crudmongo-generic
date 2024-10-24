using Application.Interfaces.Infrastructure;
using Core.Entities;
using Infrastructure.Service.Mongo.Database.Context;
using Infrastructure.Service.Mongo.Database.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection Infrastructure(this IServiceCollection service)
        {
            service.AddScoped<IPeopleRepository<People>, PeopleRepository<People>>();

            return service;
        }

        public static IServiceCollection RegisterMongo(this IServiceCollection services,
                                                       string connectionString,
                                                       string databaseName,
                                                       string collectionName) =>
            services.AddSingleton(cfg => new MongoContext(connectionString, databaseName, collectionName));
    }
}