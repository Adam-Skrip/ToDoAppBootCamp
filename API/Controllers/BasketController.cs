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
    public async Task<IActionResult> GetBasketsAsync(CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        List<QuestBasketModel> listOfBaskets = await _basketService.GetAllAsync(user, ct);
        return StatusCode(StatusCodes.Status200OK, listOfBaskets);
    }

    [HttpPost("new")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(QuestBasketDto))]
    [SwaggerOperation(
        summary: "Create Basket",
        description: "Creates Basket in the database",
        OperationId = "createBasket",
        Tags = new[] { "Basket API" })]
    public async Task<IActionResult> CreateBasket
        (QuestBasketDto basketDto, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestBasketModel dto = await _basketService.CreateAsync(user, basketDto, ct);
        return StatusCode(StatusCodes.Status201Created, dto);
    }

    [HttpGet("name")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestBasketDto))]
    [SwaggerOperation(
        summary: "Retrieve One Basket",
        description: "Returns basket from the database",
        OperationId = "getOneBasket",
        Tags = new[] { "Basket API" })]
    public async Task<IActionResult> getBasketAsync(Guid publicId, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestBasketModel qBasket = await _basketService.GetAsync(user, publicId, ct);
        return StatusCode(StatusCodes.Status200OK, qBasket);
    }

    [HttpDelete("remove")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestBasketDto))]
    [SwaggerOperation(
        summary: "Delete One Basket",
        description: "Deletes basket from the database",
        OperationId = "deleteOneBasket",
        Tags = new[] { "Basket API" })]
    public async Task<IActionResult> DeleteBasketAsync(Guid publicId, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        await _basketService.DeleteAsync(user, publicId, ct);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPut("update")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestBasketDto))]
    [SwaggerOperation(
        summary: "Update Basket Name",
        description: "Update basket name in the database",
        OperationId = "UpdateOneBasket",
        Tags = new[] { "Basket API" })]
    public async Task<IActionResult> UpdateBasketName(Guid publicId, string newBasket, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestBasketModel qBasket = await _basketService.UpdateAsync(user, newBasket, publicId, ct);
        return StatusCode(StatusCodes.Status200OK, qBasket);
    }
    
    [HttpPut("migrate")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerOperation(
        summary: "Migrate task to another basket",
        description: "Migrate task to another basket in the database",
        OperationId = "MigrateOneTask",
        Tags = new[] { "Basket API" })]
    public async Task<IActionResult> MigrateTask(Guid oldBasketId, Guid newBasketId, Guid questId, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestModel qBasket = await _basketService.MigrateTask(user, oldBasketId,newBasketId,questId, ct);
        return StatusCode(StatusCodes.Status200OK, qBasket);
    }
}