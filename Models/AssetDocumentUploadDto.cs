using Microsoft.AspNetCore.Http; // Required for IFormFile

namespace RoadSafe.API.Models
{
    public class AssetDocumentUploadDto
    {
        public int AssetId { get; set; }
        public string? DocType { get; set; }
        public IFormFile? File { get; set; } 
    }
}