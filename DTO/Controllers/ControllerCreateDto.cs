using System;

namespace RoadSafe.API.DTOs
{
   public class ControllerCreateDto
    {
        public int IntersectionId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string FirmwareVersion { get; set; } = string.Empty;
        public DateTime? InstallDate { get; set; }
    }
}