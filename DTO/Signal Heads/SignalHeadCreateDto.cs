namespace RoadSafe.API.DTOs
{
   public class SignalHeadCreateDto
    {
        public int ControllerId { get; set; }
        public string Approach { get; set; } = string.Empty;
        public string LampType { get; set; } = string.Empty;
    }
}