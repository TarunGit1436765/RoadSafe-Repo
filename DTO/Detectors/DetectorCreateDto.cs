namespace RoadSafe.API.DTOs
{
    public class DetectorCreateDto
    {
        public int ControllerId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string LocationDesc { get; set; } = string.Empty;
    }
}