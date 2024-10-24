using Application.Common.Extensions.Middlewares;
using Application.Common.Helpers.Serializer;
using Application.Common.Utilities;
using Infrastructure;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PeopleRegistration.Service.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IWebHostEnvironment environment = builder.Environment;

IConfiguration configuration = builder.Configuration;

#region Configuration

builder.Configuration.AddAppSetting(environment);

string connectionString = configuration["AppSettings:ConnectionStringMongo"];

builder.Configuration
    .AddMongoProvider(connectionString,
                      configuration["AppSettings:DatabaseName"],
                      configuration["MongoConfigurationProvider:CollectionName"],
                      Convert.ToBoolean(configuration["MongoConfigurationProvider:ReloadOnChange"]),
                      nameof(BusinessSettings)
    );

#endregion Configuration

#region Service - Host

builder.Services
    .Configure<BusinessSettings>(builder.Configuration
    .GetRequiredSection(nameof(BusinessSettings)));

BusinessSettings settings = builder.Configuration
    .GetSection(nameof(BusinessSettings))
    .Get<BusinessSettings>();

builder.Host
    .ConfigureSerilog(settings.LogLevelSink,
                      configuration["AppSettings:DatabaseName"],
                      settings.SinkCollectionName,
                      connectionString,
                      settings.DocumentExpiration
    );

builder.Services
    .RegisterService()
    .RegisterAutoMapper()
    .RegisterHealthCheck()
    .RegisterMongo(connectionString,
                   configuration["AppSettings:DatabaseName"],
                   configuration["AppSettings:CollectionName"]
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

string[] mongoHealthCheck = ["MongoDB", "Mongo"];

builder.Services
    .AddHealthChecks()
    .AddMongoDb(connectionString,
                configuration["AppSettings:DatabaseName"],
                name: "Mongo",
                failureStatus: HealthStatus.Unhealthy,
                tags: mongoHealthCheck
    );

#endregion Service - Host

#region Middleware

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks(settings.HealthChecksEndPoint);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<CustomHeadersMiddleware>();

app.MapControllers();

#region Logging

ILogger logger = app.Logger;

IHostApplicationLifetime lifetime = app.Lifetime;

lifetime.ApplicationStarted.Register(() =>
{
    ProgramInformation information = new()
    {
        ApplicationName = environment.ApplicationName,
        HealthChecksEndPoint = settings.HealthChecksEndPoint,
        EnvironmentName = environment.EnvironmentName,
        Date = DateTime.UtcNow.ToLocalTime()
    };

    logger.LogInformation("=============== PROGRAM ============= \n" +
                          "{Information} \n" +
                          "============= END PROGRAM ===========",
                          SerializerObject.ConvertObjectToJsonIndented(information)
    );
});

#endregion Logging

await app.RunAsync();

#endregion Middleware