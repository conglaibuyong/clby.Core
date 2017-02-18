using clby.Core.Misc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace clby.Core.MongoDB
{
    public static class MongoDBServicesExtensions
    {
        public static IServiceCollection AddMongoDBService(this IServiceCollection services, Action<MongoDBOptions> setupAction)
        {
            Ensure.IsNotNull(services, "services");
            Ensure.IsNotNull(setupAction, "setupAction");

            services.AddSingleton<IMongoDbHelper, MongoDbHelper>();

            OptionsServiceCollectionExtensions.AddOptions(services);
            services.Add(ServiceDescriptor.Singleton<IMongoDbHelper, MongoDbHelper>());
            OptionsServiceCollectionExtensions.Configure<MongoDBOptions>(services, setupAction);
            return services;
        }
    }
}
