using FaceAI.Infrastructure;
using FaceAI.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using FaceAI.WebApi;
using FaceAI.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using FaceAI.Application.Middleware;

var builder = WebApplication.CreateBuilder(args);
AppHelper.ConfigApplication(builder, args);
// Add services to the container.
builder.Services.ConfigSystemInfo(builder.Configuration);
builder.Services.AddApplicationServices() 
 .AddInfrastructureServices(builder.Configuration)
 .AddAppilcationApiVersioning()
 .AddSwagger()
 .AddCustomAuthentication(builder.Configuration)
 .AddAccessRequestApiClient(builder.Configuration)
 .AddHealthChecks().Services.AddDbContext<PhotoDbContext>();

var origins = builder.Configuration.GetSection("Cors:Origins").Value;
var methods = builder.Configuration.GetSection("Cors:Methods").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        corsBuilder =>
        {
            corsBuilder.WithOrigins(origins)
                                .AllowAnyHeader()
                                .WithMethods(methods);
        });
});

builder.Services.AddControllers();
//add SeriLog
builder.Host.UseSerilog(((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration)
    .WriteTo.Console();
}));
var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();
// Configure the HTTP request pipeline.
app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
    .WithOrigins(origins)
    .WithMethods(methods)
    .AllowAnyHeader();
});

app.UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHealthChecks("/health",
            new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).WithMetadata(new AllowAnonymousAttribute());
    });

app.EnableSwagger();
Console.WriteLine("Running the server.....");
app.Run();


public partial class Program { }
