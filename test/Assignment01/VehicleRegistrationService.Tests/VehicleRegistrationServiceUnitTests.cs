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

            Stream streamTask;
            try {
                streamTask = await client.GetStreamAsync($"http://127.0.0.1:6002/vehicleinfo/{VEHICLE_ID}");
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to query endpoint. Error: {ex.Message}");
            }

            JsonDocument actualResult;

            try {
                actualResult = await JsonSerializer.DeserializeAsync<JsonDocument>(streamTask);
            }
            catch (Exception ex) {
                throw new XunitException($"Unable to parse result. Error: {ex.Message}");
            }

            Assert.Equal(VEHICLE_ID, actualResult.RootElement.GetProperty("vehicleId").GetString());
        }
    }
}
