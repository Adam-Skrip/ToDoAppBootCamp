using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    
    [HttpPost("new")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(QuestModel))]
    [SwaggerOperation(
        summary: "Create Task",
        description: "Creates task in the database",
        OperationId = "createTask",
        Tags = new[] { "Task API" })]
    public async Task<IActionResult> CreateTask
        (QuestDto questDto,Guid basketId, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestModel dto = await _taskService.CreateAsync(user,basketId ,questDto, ct);
        return StatusCode(StatusCodes.Status201Created, dto);
    }
    
    [HttpPost("{publicId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestModel))]
    [SwaggerOperation(
        summary: "Retrieve Task",
        description: "Retrieves task in the database",
        OperationId = "getTask",
        Tags = new[] { "Task API" })]
    public async Task<IActionResult> getTask
        (Guid publicId, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestModel dto = await _taskService.GetAsync(user,publicId, ct);
        return StatusCode(StatusCodes.Status201Created, dto);
    }
    
    [HttpDelete("remove")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestDto))]
    [SwaggerOperation(
        summary: "Delete One Task",
        description: "Deletes Task from the database",
        OperationId = "delteOneTask",
        Tags = new[] { "Task API" })]
    public async Task<IActionResult> DeleteTaskAsync(Guid publicId, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        await _taskService.DeleteAsync(user, publicId, ct);
        return StatusCode(StatusCodes.Status200OK);
    }
    
    [HttpPut("update")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestDto))]
    [SwaggerOperation(
        summary: "Update Task",
        description: "Update task in the database",
        OperationId = "UpdateOneTask",
        Tags = new[] { "Task API" })]
    public async Task<IActionResult> UpdateBasketName(QuestDto dto,Guid questId, CancellationToken ct)
    {
        var user = HttpContext.User.Identity!.Name;

        QuestModel quest = await _taskService.UpdateAsync(user,dto,questId, ct);
        return StatusCode(StatusCodes.Status200OK, quest);
    }
}