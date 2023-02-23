using API.Entities.DTO;
using API.Entities.Models;

namespace API.Services.Interfaces;

public interface ITaskService
{
   
    Task<QuestModel> GetAsync(string username,Guid questId,CancellationToken ct = default);
    
    Task<QuestModel> CreateAsync(string username,Guid basketId,QuestDto dto,CancellationToken ct = default);
    
    Task DeleteAsync(string username,Guid taskId,CancellationToken ct = default);
    
    Task<QuestModel> UpdateAsync(string username, QuestDto questDto, Guid publicId, CancellationToken ct);


}