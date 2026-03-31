namespace RoadSafe.API.DTOs
{
	public class DetectorCreateDto
	{
		public int ControllerId { get; set; }
		public string Type { get; set; }
		public string LocationDesc { get; set; }
		
		public string Status { get; set; }  
	}
}