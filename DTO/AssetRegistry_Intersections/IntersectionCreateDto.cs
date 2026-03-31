namespace RoadSafe.API.DTOs
{
	public class IntersectionCreateDto
	{
		public string Name { get; set; }
		public string Coordinates { get; set; }
		public string Region { get; set; }
		public string Type { get; set; }
		
		public string Status { get; set; }  // <-- NEW
	}
}