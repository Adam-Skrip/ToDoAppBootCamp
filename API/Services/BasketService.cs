﻿using API.Database;
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
            name = basket.name,
            UserId = user.Id
        };

        await _context.Baskets.AddAsync(questBasket, ct);
        await _context.SaveChangesAsync(ct);

        return questBasket.ToDto();
    }

    public async Task<QuestBasketModel> GetAsync(string username, string basket, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        if (basket == null)
        {
            throw new Exception("Basket is not provided!");
        }

        QuestBasket Qbasket = await _context.Baskets
            .AsNoTracking()
            .Include(q => q.Quests)
            .Where(u => u.UserId == user.Id)
            .SingleOrDefaultAsync(b => b.name == basket, ct);

        if (Qbasket == null)
        {
            throw new Exception($"Basket with name: {basket} does not exist!");
        }

        var questModel = Qbasket.ToDto();
        return questModel;
    }

    public async Task<QuestBasketModel> UpdateAsync(string username,string newBasket, string oldBasket, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }

        if (oldBasket == null)
        {
            throw new Exception("Basket is not provided!");
        }
        
        if (newBasket == null)
        {
            throw new Exception("New Basket name is not provided!");
        }
        
        QuestBasket Qbasket = await _context.Baskets
            .AsNoTracking().SingleOrDefaultAsync(b => b.name == oldBasket, ct);

        if (Qbasket == null)
        {
            throw new Exception($"Basket with name: {oldBasket} does not exist!");
        }
        
        Qbasket.name = newBasket;

        _context.Baskets.Update(Qbasket);
        await _context.SaveChangesAsync(ct);

        return Qbasket.ToDto();

    }

    public async Task DeleteAsync(string username, string basket, CancellationToken ct)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            throw new Exception("User doesnt exist!");
        }
        
        if (basket == null)
        {
            throw new Exception("Basket is not provided!");
        }

        var basketFound = await _context.Baskets
            .AsNoTracking()
            .Include(q => q.Quests)
            .Where(u => u.UserId == user.Id)
            .SingleOrDefaultAsync(b => b.name == basket, ct);
        
        if (basketFound == null)
        {
            throw new Exception($"Basket with name: {basket} does not exist!");
        }

        _context.Baskets.Remove(basketFound);
        await _context.SaveChangesAsync(ct);
    }
}