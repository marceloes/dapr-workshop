using System;

namespace TrafficControlService.Tests
{
    public class VehicleState
    {
        public string LicenseNumber { get; set; }
        public DateTime EntryTimestamp { get; set; }
        public DateTime ExitTimestamp { get; set; }
    }
}