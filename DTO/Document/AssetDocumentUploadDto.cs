using Microsoft.AspNetCore.Http;

namespace RoadSafe.API.DTOs
{
    public class AssetDocumentUploadDto
    {
        public int AssetId { get; set; }
        public string DocType { get; set; } = string.Empty;
        public IFormFile File { get; set; } = null!;
    }
}