namespace RoadSafe.API.DTOs
{
   public class DetectorResponseDto
    {
        public int DetectorId { get; set; }
        public int ControllerId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string LocationDesc { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}