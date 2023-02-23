using API.Entities.DTO;
using API.Entities.Models;

namespace API.Services.Interfaces;

public interface IBasketService
{
    Task<List<QuestBasketModel>> GetAllAsync(string username, CancellationToken ct);

    Task<QuestBasketModel> GetAsync(string username, string basket, CancellationToken ct);

    Task<QuestBasketModel> CreateAsync(string username, QuestBasketDto basket, CancellationToken ct);

    Task<QuestBasketModel> UpdateAsync(string username, string newBasket, string oldBasket, CancellationToken ct);

    Task DeleteAsync(string username, string basket, CancellationToken ct);
}