using API.Entities.DTO;
using API.Entities.Models;

namespace API.Services.Interfaces;

public interface ITaskService
{
    Task<List<QuestDto>> GetAllAsync(CancellationToken ct = default);
    
    Task<QuestDto> GetAsync(Guid? taskId ,CancellationToken ct = default);
    
    Task<QuestDto> CreateAsync(QuestDto dto,CancellationToken ct = default);
}