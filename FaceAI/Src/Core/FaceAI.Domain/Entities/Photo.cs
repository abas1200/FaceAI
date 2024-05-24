using FaceAI.Domain.Common;

namespace FaceAI.Domain.Entities;

public class Photo: EntityBase
{
    public Guid PhotoId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}