using System;

namespace RoadSafe.API.DTOs
{
    public class ControllerResponseDto
    {
        public int ControllerId { get; set; }
        public int IntersectionId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
        public DateTime? InstallDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}