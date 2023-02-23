using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.DTO;
using API.Entities.Models;

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
   
    public int BasketId { get; set; }
    
    public QuestBasket Basket { get; set; }

    public QuestModel ToDto()
    {
        return new QuestModel
        {
            PublicId = PublicId,
            Title = Title,
            Description = Description,
            CreatedAt = CreatedAt,
            Status = Status
        };
    }
}