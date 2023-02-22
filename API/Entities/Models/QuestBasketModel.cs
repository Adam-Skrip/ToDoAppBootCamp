using API.Entities.Domain;

namespace API.Entities.Models;

public class QuestBasketModel
{
    public string Name { get; set; }

    public List<QuestModel> Quests { get; set; }
}