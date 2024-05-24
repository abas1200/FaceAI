using Microsoft.Extensions.Configuration;
using FaceAI.Application.Authorization;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Reflection;
using FaceAI.Application.Authorization.Option;


namespace FaceAI.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
 
            return services;
        }

        public static IServiceCollection AddAccessRequestApiClient(this IServiceCollection services, ConfigurationManager configuration)
        {
            var accessRequestApiOption = new AccessRequestApiOption();

            // Get configuration section
            configuration.GetSection(AccessRequestApiOption.SectionName).Bind(accessRequestApiOption);
            // Bind option with configuration
            services.Configure<AccessRequestApiOption>(configuration.GetSection(AccessRequestApiOption.SectionName).Bind);

            services
                .AddScoped(typeof(AuthHeaderHandler<>))
                .AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(c => c.BaseAddress = accessRequestApiOption.TokenEndpoint.BaseUrl)
                .AddHttpMessageHandler<AuthHeaderHandler<AccessRequestApiOption>>(); //Add header handler which take care NSS token adding

            return services;
        }


    }
}
