using System;

namespace FineCollectionService.Tests
{
    public class SpeedingViolation
    {
        public string vehicleId { get; set; }
        public string roadId { get; set; }
        public int violationInKmh { get; set; }
        public DateTime timestamp { get; set; }
    }
}