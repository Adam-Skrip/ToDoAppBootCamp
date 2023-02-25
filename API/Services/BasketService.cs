using API.Database;
using API.Entities.Domain;
using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class BasketService : IBasketService
{
    private ApplicationContext _context;

    public BasketService(ApplicationContext context)
    {
        _context = context;
    }


    public async Task<List<QuestBasketModel>> GetAllAsync(string username, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username, ct);

        if (user == null)
        {
            throw new Exception("User doesnt exists!");
        }

        List<QuestBasket> baskets = await _context.Baskets.Include(q => q.Quests).AsNoTracking()
            .Where(u => u.UserId == user.Id).ToListAsync(ct);
        List<QuestBasketModel> dbaskets = baskets.Select(o => o.ToDto()).ToList();

        return dbaskets;
    }

    public async Task<QuestBasketModel> CreateAsync(string username, QuestBasketDto basket, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        var questBasket = new QuestBasket
        {
            PublicId = Guid.NewGuid(),
            name = basket.name,
            UserId = user.Id
        };

        await _context.Baskets.AddAsync(questBasket, ct);
        await _context.SaveChangesAsync(ct);

        return questBasket.ToDto();
    }

    public async Task<QuestBasketModel> GetAsync(string username, Guid publicId, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        if (publicId == null)
        {
            throw new Exception("Basket is not provided!");
        }

        QuestBasket Qbasket = await _context.Baskets
            .AsNoTracking()
            .Include(q => q.Quests)
            .Where(u => u.UserId == user.Id)
            .SingleOrDefaultAsync(b => b.PublicId == publicId, ct);

        if (Qbasket == null)
        {
            throw new Exception($"Basket with Id: {publicId} does not exist!");
        }

        var questModel = Qbasket.ToDto();
        return questModel;
    }

    public async Task<QuestBasketModel> UpdateAsync(string username,string newBasket, Guid publicId, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        if (publicId == null)
        {
            throw new Exception("Basket is not provided!");
        }
        
        if (newBasket == null)
        {
            throw new Exception("New Basket name is not provided!");
        }
        
        QuestBasket Qbasket = await _context.Baskets
            .AsNoTracking().SingleOrDefaultAsync(b => b.PublicId == publicId, ct);

        if (Qbasket == null)
        {
            throw new Exception($"Basket with Id: {publicId} does not exist!");
        }
        
        Qbasket.name = newBasket;

        _context.Baskets.Update(Qbasket);
        await _context.SaveChangesAsync(ct);

        return Qbasket.ToDto();

    }

    public async Task DeleteAsync(string username, Guid publicId, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }
        
        if (publicId == null)
        {
            throw new Exception("Basket is not provided!");
        }

        var basketFound = await _context.Baskets
            .AsNoTracking()
            .Include(q => q.Quests)
            .Where(u => u.UserId == user.Id)
            .SingleOrDefaultAsync(b => b.PublicId == publicId, ct);
        
        if (basketFound == null)
        {
            throw new Exception($"Basket with Id: {publicId} does not exist!");
        }

        _context.Baskets.Remove(basketFound);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<QuestModel> MigrateTask(string username, Guid oldBasketId, Guid newBasketId, Guid questId, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        var findBasket = await _context.Baskets.SingleOrDefaultAsync(x => x.PublicId == newBasketId, ct);

        if (findBasket == null)
        {
            throw new Exception($"Basket with Id:{newBasketId} does not exists!");
        }
        
        Quest findQuest = await _context.Quests
            .AsNoTracking()
            .SingleOrDefaultAsync(q => q.PublicId == questId, ct);

        if (findQuest == null)
        {
            throw new Exception($"Quest with Id: {questId} does not exists!");
        }

        var migrate = new Quest
        {
            PublicId = Guid.NewGuid(),
            Title = findQuest.Title,
            Description = findQuest.Description,
            CreatedAt = findQuest.CreatedAt,
            Status = findQuest.Status,
            BasketId = findBasket.id
        };


        await _context.Quests.AddAsync(migrate, ct);
        _context.Quests.Remove(findQuest);
        await _context.SaveChangesAsync(ct);
        
        return migrate.ToDto();
        
    }
}