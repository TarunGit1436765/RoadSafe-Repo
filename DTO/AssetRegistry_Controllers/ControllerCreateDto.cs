using System;

namespace RoadSafe.API.DTOs
{
	public class ControllerCreateDto
	{
		public int IntersectionId { get; set; }
		public string Model { get; set; }
		public string FirmwareVersion { get; set; }
		public DateTime? InstallDate { get; set; }

		 
		public string Status { get; set; }  
	}
}