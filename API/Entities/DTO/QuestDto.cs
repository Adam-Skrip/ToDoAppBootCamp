namespace API.Entities.DTO;

public class QuestDto
{
    public Guid PublicId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
}