namespace RoadSafe.API.DTOs
{
	public class SignalHeadCreateDto
	{
		public int ControllerId { get; set; }
		public string Approach { get; set; }
		public string LampType { get; set; }

		
		public string Status { get; set; }  // <-- NEW
	}
}