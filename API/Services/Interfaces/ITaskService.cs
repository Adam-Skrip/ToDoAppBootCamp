using API.Entities.DTO;
using API.Entities.Models;

namespace API.Services.Interfaces;

public interface ITaskService
{
    Task<List<QuestModel>> GetAllAsync(string username,CancellationToken ct = default);
    
    Task<QuestDto> GetAsync(string username,QuestBasketModel basket,string name,CancellationToken ct = default);
    
    Task<QuestModel> CreateAsync(string username,string basket,QuestDto dto,CancellationToken ct = default);
    
    Task<QuestModel> DeleteAsync(string username,QuestBasketModel basket,string name,CancellationToken ct = default);

}