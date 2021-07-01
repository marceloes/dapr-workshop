using System;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit.Sdk;
using System.Text;
using System.Net.Http.Headers;
using System.IO;
using System.Text.Json;

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

        [Fact]
        public async Task EntryCamStateStoreTest()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            const string LICENSE_NUMBER = "XT-346-Y";
            const string ENTRY_TIMESTAMP = "2020-09-10T10:38:47";

            HttpContent httpContent = new StringContent(String.Concat(@"{ ""lane"": 1, ""licenseNumber"": """, LICENSE_NUMBER, @""", ""timestamp"": """, ENTRY_TIMESTAMP, @""" }"), 
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

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Stream streamTask;
            try {
                streamTask = await client.GetStreamAsync($"http://localhost:3600/v1.0/state/statestore/{LICENSE_NUMBER}");
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: {ex.Message}");
            }
            
            VehicleState actualResult;

            try {
                actualResult = await JsonSerializer.DeserializeAsync<VehicleState>(streamTask);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to parse result. Error: {ex.Message}");
            }         

            Assert.Equal(ENTRY_TIMESTAMP, actualResult.EntryTimestamp.ToString("s"));
        }

        [Fact]
        public async Task ExitCamStateStoreTest()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            const string LICENSE_NUMBER = "XT-346-Y";
            const string EXIT_TIMESTAMP = "2020-09-10T10:38:52";

            HttpContent httpContent = new StringContent(String.Concat(@"{ ""lane"": 1, ""licenseNumber"": """, LICENSE_NUMBER, @""", ""timestamp"": """, EXIT_TIMESTAMP, @""" }"), 
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

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Stream streamTask;
            try {
                streamTask = await client.GetStreamAsync($"http://localhost:3600/v1.0/state/statestore/{LICENSE_NUMBER}");
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: {ex.Message}");
            }
            
            VehicleState actualResult;

            try {
                actualResult = await JsonSerializer.DeserializeAsync<VehicleState>(streamTask);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to parse result. Error: {ex.Message}");
            }         

            Assert.Equal(EXIT_TIMESTAMP, actualResult.ExitTimestamp.ToString("s"));
        }
    }
}
