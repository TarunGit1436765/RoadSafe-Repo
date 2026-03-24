namespace RoadSafe.API.DTOs
{
    public class IntersectionCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Coordinates { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}