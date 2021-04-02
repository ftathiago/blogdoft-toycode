using FluentAssertions;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.InfraData.Models;
using WebApi.IntegrationTest.Fixtures;
using WebApi.IntegrationTest.Fixtures.DataFixtures;
using Xunit;

namespace WebApi.IntegrationTest.Controllers
{
    public class SampleControllerTest : IClassFixture<HostFactory<Startup>>
    {
        private readonly HttpClient _client;

        public SampleControllerTest(HostFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            var expectedResponse = SampleObject
                .SampleTables()
                .First();
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Sample/{expectedResponse.Id}");

            var response = await _client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            JsonSerializer.Deserialize<SampleTable>(body, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            })
            .Should().BeEquivalentTo(expectedResponse);
        }
    }
}
