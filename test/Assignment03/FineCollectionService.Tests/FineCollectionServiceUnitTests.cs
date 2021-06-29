using System;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit.Sdk;
using System.Text;
using System.IO;
using System.Text.Json;

namespace FineCollectionService.Tests
{
    public class FineCollectionServiceUnitTests
    {
        private static readonly HttpClient client = new HttpClient();
        [Fact]
        public async Task CollectFineTest()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            var data = new SpeedingViolation{
                vehicleId = "RT-318-K",
                roadId = "A12",
                violationInKmh = 15,
                timestamp = new DateTime(2020, 09, 20, 08, 33, 41)
            };

            var cloudEvent = new CloudEvent<SpeedingViolation> {
                id = Guid.NewGuid().ToString(),
                type = "com.dapr.event.sent",
                datacontenttype = "application/json; charset=utf-8",
                specversion = "1.0",
                data = data,
                source = "TrafficControlService",
                pubsubname = "pubsub",
                topic = "collectfine"
            };

            var json = JsonSerializer.Serialize(cloudEvent);

            HttpContent httpContent = new StringContent(json,
                                                        Encoding.UTF8, 
                                                        "application/json");

            HttpResponseMessage httpResponseMessage;
            try {
                httpResponseMessage = await client.PostAsync("http://localhost:3601/v1.0/invoke/FineCollectionService/method/collectfine", httpContent);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: ${ex.Message}");
            }            

            Assert.True(httpResponseMessage.IsSuccessStatusCode, httpResponseMessage.ReasonPhrase);
        }

        [Fact]
        public async Task SubscribeTest()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Task<Stream> streamTask;
            try {
                streamTask = client.GetStreamAsync($"http://localhost:3601/v1.0/invoke/FineCollectionService/method/dapr/subscribe");
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: {ex.Message}");
            }

            JsonDocument actualResult;

            try {
                actualResult = await JsonSerializer.DeserializeAsync<JsonDocument>(await streamTask);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to parse result. Error: {ex.Message}");
            }

            Assert.True(streamTask.IsCompletedSuccessfully);

            Assert.Equal(1, actualResult.RootElement.GetArrayLength());
            Assert.Equal("pubsub", actualResult.RootElement[0].GetProperty("pubsubname").GetString());
            Assert.Equal("collectfine", actualResult.RootElement[0].GetProperty("topic").GetString());
            Assert.Equal("/collectfine", actualResult.RootElement[0].GetProperty("route").GetString());
        } 
    }
}
