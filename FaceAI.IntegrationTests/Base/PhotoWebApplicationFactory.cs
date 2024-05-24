using FaceAI.Domain.Entities;
using FaceAI.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FaceAI.IntegrationTests.Base
{
    public class PhotoWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                services.AddDbContext<PhotoDbContext>(options =>
                {
                    options.UseInMemoryDatabase("PhotoDbContextInMemoryTest");
                });

                var sprovider = services.BuildServiceProvider();


                using (var scope = sprovider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<PhotoDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<PhotoWebApplicationFactory<TStartup>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                };

            });
        }
        public HttpClient GetClient()
        {
            return CreateClient();
        }

        public static void InitializeDbForTests(PhotoDbContext context)
        {
            var PhotoId = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");

            context.Photos.Add(new Photo
            {
                PhotoId = PhotoId,
                Description = "test desc",
                CreatedBy = "test",
                CreatedDate = DateTime.Now,
                FileName = "test.jpg"
            });

            context.SaveChanges();
        }

    }
}
