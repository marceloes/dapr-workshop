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
            const string VEHICLE_ID = "KZ-49-VX";

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Task<Stream> streamTask;
            try {
                streamTask = client.GetStreamAsync($"http://localhost:3602/v1.0/invoke/VehicleRegistrationService/method/vehicleinfo/{VEHICLE_ID}");
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

            Assert.Equal(VEHICLE_ID, actualResult.RootElement.GetProperty("vehicleId").GetString());
        }
    }
}
