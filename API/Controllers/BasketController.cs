using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestBasketDto))]
    [SwaggerOperation(
        summary: "Retrieve Baskets",
        description: "Returns all baskets in the database",
        OperationId = "getAllBaskets",
        Tags = new[] { "Basket API" })]
    public async Task<IActionResult> GetTasksAsync(CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        List<QuestBasketModel> listOfBaskets = await _basketService.GetAllAsync(user, ct);
        return Ok(listOfBaskets);
    }
    
    [HttpPost("new")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestBasketDto))]
    [SwaggerOperation(
        summary: "Create Basket",
        description: "Creates Basket in the database",
        OperationId = "createBasket",
        Tags = new[] { "Basket API" })]
    public async Task<IActionResult> CreateTask
        (QuestBasketDto basketDto, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestBasketModel dto = await _basketService.CreateAsync(user, basketDto, ct);
        return StatusCode(StatusCodes.Status201Created, dto);
    }

}