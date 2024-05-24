using FaceAI.IntegrationTests.Base;
using System.Net;


namespace FaceAI.IntegrationTests.V1.Controllers
{
    public class PhotoControllerTests : PhotoWebApplicationFactory<Program>
    {
        private PhotoWebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new PhotoWebApplicationFactory<Program>();
        }
         
        [SetUp]
        public void Setup()
        {
            _client = _factory.CreateClient();
        } 

        [Test]
        public async Task Returns200()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "/api/Photo?UserName=test");

            using var response = await _client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}
