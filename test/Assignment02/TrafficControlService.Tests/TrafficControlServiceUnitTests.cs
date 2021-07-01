using System;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit.Sdk;
using System.Text;

namespace TrafficControlService.Tests
{
    public class TrafficControlServiceUnitTests
    {
        private static readonly HttpClient client = new HttpClient();

        [Fact]
        public async Task EntryCamTest()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            HttpContent httpContent = new StringContent(@"{ ""lane"": 1, ""licenseNumber"": ""XT-346-Y"", ""timestamp"": ""2020-09-10T10:38:47"" }", 
                                                        Encoding.UTF8, 
                                                        "application/json");

            HttpResponseMessage httpResponseMessage;
            try {
                httpResponseMessage = await client.PostAsync("http://localhost:3600/v1.0/invoke/trafficcontrolservice/method/entrycam", httpContent);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: ${ex.Message}");
            }            

            Assert.True(httpResponseMessage.IsSuccessStatusCode); 
        }

        [Fact]
        public async Task ExitCamTest()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            HttpContent httpContent = new StringContent(@"{ ""lane"": 1, ""licenseNumber"": ""XT-346-Y"", ""timestamp"": ""2020-09-10T10:38:52"" }", 
                                                        Encoding.UTF8, 
                                                        "application/json");

            HttpResponseMessage httpResponseMessage;
            try {
                httpResponseMessage = await client.PostAsync("http://localhost:3600/v1.0/invoke/trafficcontrolservice/method/exitcam", httpContent);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: ${ex.Message}");
            }            

            Assert.True(httpResponseMessage.IsSuccessStatusCode); 
        }
    }
}
