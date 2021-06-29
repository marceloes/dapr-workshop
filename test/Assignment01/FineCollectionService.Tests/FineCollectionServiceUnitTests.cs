using System;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit.Sdk;
using System.Text;

namespace FineCollectionService.Tests
{
    public class FineCollectionServiceUnitTests
    {
        private static readonly HttpClient client = new HttpClient();
        [Fact]
        public async Task CollectFine()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            HttpContent httpContent = new StringContent(@"{ ""vehicleId"": ""RT-318-K"", ""roadId"": ""A12"", ""violationInKmh"": 15, ""timestamp"": ""2020-09-20T08:33:41"" }", 
                                                        Encoding.UTF8, 
                                                        "application/json");

            HttpResponseMessage httpResponseMessage;
            try {
                httpResponseMessage = await client.PostAsync("http://127.0.0.1:6001/collectfine", httpContent);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: ${ex.Message}");
            }            

            Assert.True(httpResponseMessage.IsSuccessStatusCode);
        }
    }
}
