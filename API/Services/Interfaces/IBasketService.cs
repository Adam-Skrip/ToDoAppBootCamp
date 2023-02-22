using API.Entities.DTO;
using API.Entities.Models;

namespace API.Services.Interfaces;

public interface IBasketService
{

    Task<List<QuestBasketModel>> GetAllAsync(string username, CancellationToken ct);

    Task<QuestBasketModel> CreateAsync(string username, QuestBasketDto basket, CancellationToken ct);
    
}