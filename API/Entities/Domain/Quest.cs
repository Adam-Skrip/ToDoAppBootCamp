using System.ComponentModel.DataAnnotations;
using API.Entities.DTO;

namespace API.Entities.Domain;

public class Quest
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public Guid? PublicId { get; set; }
    
    [Required,StringLength(30)]
    public string Title { get; set; }
    
    [StringLength(1024)]
    public string Description { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }

    [Required] 
    public string Status { get; set; } = "Not Assigned";

    public QuestDto ToDto()
    {
        return new QuestDto
        {
            PublicId = PublicId.Value,
            Title = Title,
            Description = Description,
            CreatedAt = CreatedAt,
            Status = Status
        };
    }
}