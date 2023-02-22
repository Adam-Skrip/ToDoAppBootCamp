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
        var user = await _context.Users.AsNoTracking().AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exists!");
        }

        List<QuestBasket> baskets = await _context.Baskets.Include(q=> q.Quests).AsNoTracking().Where(u => u.UserId == user.Id).ToListAsync(ct);
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
           name = basket.name,
           UserId = user.Id
        };

        await _context.Baskets.AddAsync(questBasket, ct);
        await _context.SaveChangesAsync(ct);

        return questBasket.ToDto();
    }
}