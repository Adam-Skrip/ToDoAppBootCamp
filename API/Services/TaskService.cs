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

    public async Task<List<QuestModel>> GetAllAsync(string username, CancellationToken ct = default)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        List<Quest> tasks = await _context.Quests.AsNoTracking().Where(u => u.BasketId == user.Id).ToListAsync(ct);
        List<QuestModel> dtasks = tasks.Select(o => o.ToDto()).ToList();

        return dtasks;
    }

    public async Task<QuestDto> GetAsync(string username, QuestBasketModel basket, string name,
        CancellationToken ct = default)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        QuestBasket findBasket = await _context.Baskets
            .AsNoTracking()
            .Include(q => q.Quests)
            .Where(u => u.UserId == user.Id)
            .SingleOrDefaultAsync(b => b.name == basket.Name, ct);

        if (findBasket == null)
        {
            throw new Exception("Basket with that name does not exists!");
        }

        // Quest findQuest = await _context.Quests.AsNoTracking();
        throw new Exception("notimeplemnted!");
    }

    public async Task<QuestModel> CreateAsync(string username, string basket, QuestDto dto,
        CancellationToken ct = default)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        var findBasket = await _context.Baskets.SingleOrDefaultAsync(x => x.name == basket, ct);

        if (findBasket == null)
        {
            throw new Exception("Basket with that name does not exists!");
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

    public async Task<QuestModel> DeleteAsync(string username, QuestBasketModel basket, string name,
        CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}