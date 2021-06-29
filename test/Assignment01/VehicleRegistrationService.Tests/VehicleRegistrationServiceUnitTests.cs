using System;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using VehicleRegistrationService.Models;
using System.IO;
using Xunit.Sdk;

namespace VehicleRegistrationService.Tests
{
    public class VehicleRegistrationServiceUnitTests
    {
        private static readonly HttpClient client = new HttpClient();

        [Fact]
        protected async Task GetVehicleInfoByLicenseNumber()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Task<Stream> streamTask;
            try {
                streamTask = client.GetStreamAsync("http://127.0.0.1:6002/vehicleinfo/KZ-49-VX");
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: ${ex.Message}");
            }

            VehicleInfo actualResult;

            try {
                actualResult = await JsonSerializer.DeserializeAsync<VehicleInfo>(await streamTask, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true});
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to parse result. Error: ${ex.Message}");
            }

            Assert.True(streamTask.IsCompletedSuccessfully);

            Assert.Equal("KZ-49-VX", actualResult.VehicleId);
        }
    }
}
