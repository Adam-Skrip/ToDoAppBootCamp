using API.Database;
using API.Entities.Domain;
using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class TaskService : ITaskService
{
    
    private ApplicationContext _context;

    public TaskService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<QuestModel>> GetAllAsync(CancellationToken ct = default)
    {
        List<Quest> tasks = await _context.Quests.AsNoTracking().ToListAsync();
        List<QuestModel> dtasks = tasks.Select(o => o.ToDto()).ToList();

        return dtasks;
    }

    public async Task<QuestDto> GetAsync(Guid? taskId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<QuestModel> CreateAsync(QuestDto dto, CancellationToken ct = default)
    {
        var quest = new Quest
        {
            PublicId = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            Status = "Not Assigned"
        };
            
        await _context.Quests.AddAsync(quest, ct);
        await _context.SaveChangesAsync();
        
        return quest.ToDto();
    }
}