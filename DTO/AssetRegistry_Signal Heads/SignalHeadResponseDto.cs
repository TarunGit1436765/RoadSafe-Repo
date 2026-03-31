namespace RoadSafe.API.DTOs
{
    public class SignalHeadResponseDto
    {
        public int SignalHeadId { get; set; }
        public int ControllerId { get; set; }
        public string Approach { get; set; } = string.Empty;
        public string LampType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}