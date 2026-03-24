namespace RoadSafe.API.DTOs
{
   public class IntersectionResponseDto
    {
        public int IntersectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Coordinates { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}