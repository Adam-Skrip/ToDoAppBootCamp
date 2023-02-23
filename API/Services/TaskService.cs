using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using API.Database;
using API.Entities.Domain;
using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class TaskService : ITaskService
{
    private ApplicationContext _context;

    public TaskService(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<QuestModel> GetAsync(string username, Guid questId, CancellationToken ct = default)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username, ct);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        Quest findQuest = await _context.Quests
            .AsNoTracking()
            .SingleOrDefaultAsync(q => q.PublicId == questId, ct);

        if (findQuest == null)
        {
            throw new Exception($"Quest with Id: {questId} does not exists!");
        }

        var questModel = findQuest.ToDto();
        return questModel;
    }

    public async Task<QuestModel> CreateAsync(string username, Guid basketId, QuestDto dto,
        CancellationToken ct = default)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username, ct);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        var findBasket = await _context.Baskets.SingleOrDefaultAsync(x => x.PublicId == basketId, ct);

        if (findBasket == null)
        {
            throw new Exception($"Basket with Id:{basketId} does not exists!");
        }

        var quest = new Quest
        {
            PublicId = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            Status = "Not Assigned",
            BasketId = findBasket.id
        };

        await _context.Quests.AddAsync(quest, ct);
        await _context.SaveChangesAsync(ct);

        return quest.ToDto();
    }

    public async Task DeleteAsync(string username, Guid taskId, CancellationToken ct = default)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username, ct);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        var quest = await _context.Quests.AsNoTracking().SingleOrDefaultAsync(q => q.PublicId == taskId, ct);

        if (quest == null)
        {
            throw new Exception($"Task with Id: {taskId} does not exist!");
        }

        _context.Quests.Remove(quest);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<QuestModel> UpdateAsync(string username, QuestDto questDto,Guid questId, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        if (questDto == null)
        {
            throw new Exception($"Invalid task request: {questDto}");
        }

        Quest uQuest = await _context.Quests
            .AsNoTracking()
            .SingleOrDefaultAsync(q => q.PublicId == questId, ct);

        if (uQuest == null)
        {
            throw new Exception($"Task with Id:{questId} was not found!");
        }

        uQuest.Title = questDto.Title;
        uQuest.Description = questDto.Description;
        uQuest.Status = questDto.Status;

        _context.Quests.Update(uQuest);
        await _context.SaveChangesAsync(ct);

        return uQuest.ToDto();
    }
}