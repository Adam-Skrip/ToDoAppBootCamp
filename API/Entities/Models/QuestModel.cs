using System.ComponentModel.DataAnnotations;
using API.Entities.Domain;

namespace API.Entities.Models;

public class QuestModel
{
    [Required, StringLength(30)] public string Title { get; set; }

    [StringLength(1024)] public string Description { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    [Required] public string Status { get; set; }

    public Quest ToDomain()
    {
        return new Quest
        {
            Title = Title,
            Description = Description,
            CreatedAt = CreatedAt,
            Status = Status
        };
    }
}