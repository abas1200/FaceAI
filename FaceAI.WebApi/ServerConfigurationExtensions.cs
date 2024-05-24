using FaceAI.Application.Authorization;
using FaceAI.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FaceAI.WebApi
{
    /// <summary>
    ///
    /// </summary>
    /// 

    public class HostOption
    {
        public string Version { get; set; } = default;
        public string SystemName { get; set; } = default;
        public string SystemDescription { get; set; } = default;
    }

    public static class AppHelper
    {
        /// <summary>
        /// building application configuration and adding following configuration files into application configuration
        /// appsettings.josn, appsettings.Env Name.josn
        /// SerilogConfig.json,  SerilogConfig.Env Name.json
        /// secrets.json
        /// command line variables
        /// environment variables
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static void ConfigApplication(WebApplicationBuilder builder, string[] args)
        {
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("host.json", false, true)
                .AddJsonFile("appsettings.json", false, true)
                //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
                 .AddJsonFile("SerilogConfig.json", false, true)
                 //.AddJsonFile($"SerilogConfig.{builder.Environment.EnvironmentName}.json", false, true)
                .AddUserSecrets<Program>(true, true)
                .AddCommandLine(args);
        }
  
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        internal static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.CustomSchemaIds(type => type.FullName);
            });

            return services;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
       internal static IServiceCollection AddAppilcationApiVersioning(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("api-version"));
            });

            return services;
        } 

        internal static WebApplication EnableSwagger(this WebApplication app)
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
 
       public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var IdentitySettings = configuration.GetSection("IdentitySettings");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = IdentitySettings.GetValue<string>("Authority");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                    options.MapInboundClaims = false;
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", IdentitySettings.GetValue<string>("Scope"));
                    policy.Requirements.Add(PermissionRequirement.AppAccess);
                });
            });

            services.AddScoped<IAuthorizationManager, AuthorizationManager>();
            services.AddScoped<IAuthorizationHandler, ApiAuthorizationHandler>();
            services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
  
            return services;
        }

        /// <summary>
        /// add client policy ( client policy )
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        //public static IServiceCollection AddCustomPolicyProvider(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var policyClientSetting = configuration.GetSection("PolicyClientSetting");
        //    services.AddClientPolicy(policyClientSetting);

        //    services.AddScoped<IClientPolicyService, ClientPolicyService>();

        //    return services;
        //}

        internal static IServiceCollection ConfigSystemInfo(this IServiceCollection services,
            IConfiguration configuration)
        {
            var version = configuration.GetSection("ApplicationVersion")?.Value;
            var applicationName = configuration.GetSection("SystemName")?.Value;
            var description = configuration.GetSection("SystemDescription")?.Value;
            if (version.IsNullOrEmpty())
            {
                throw new Exception("cannot find ApplicationVersion in host.json file");
            }
            if (applicationName.IsNullOrEmpty())
            {
                throw new Exception("cannot find SystemName in host.json file");
            }
            if (description.IsNullOrEmpty())
            {
                throw new Exception("cannot find SystemDescription in host.json file");
            }
            services.Configure<HostOption> (config =>
            {
                config.Version = version;
                config.SystemName = applicationName;
                config.SystemDescription = description;
            });

            return services;
        } 
    }
}