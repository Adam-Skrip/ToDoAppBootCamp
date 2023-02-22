using System.ComponentModel.DataAnnotations;
using API.Entities.Models;

namespace API.Entities.Domain;

public class QuestBasket
{
    [Key]
    public int id { get; set; }
    
    public string name { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; }
    
    public ICollection<Quest> Quests { get; set; }
    
    public QuestBasketModel ToDto()
    {
        return new QuestBasketModel
        {
            Name = name,
            Quests = Quests.Select(x=> x.ToDto()).ToList()
        };
    }
}