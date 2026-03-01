using Hypesoft.API.Configurations;
using Hypesoft.API.Data;
using Microsoft.Extensions.Options;

namespace Hypesoft.API.Extensions;

public static class MongoExtensions
{
    public static IServiceCollection AddMongoConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MongoSettings>(
            configuration.GetSection("Mongo"));

        services.AddSingleton<MongoContext>();

        return services;
    }
}