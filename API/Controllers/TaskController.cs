using API.Entities.DTO;
using API.Entities.Models;
using API.Services.Interfaces;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestDto))]
    [SwaggerOperation(
        summary: "Retrieve Tasks",
        description: "Returns all tasks in the database",
        OperationId = "getAllTasks",
        Tags = new[] { "Task API" })]
    public async Task<IActionResult> GetTasksAsync(CancellationToken ct)
    {
        List<QuestDto> listOfTasks = await _taskService.GetAllAsync(ct);
        return Ok(listOfTasks);
    }

    [HttpPost("new")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestDto))]
    [SwaggerOperation(
        summary: "Create Task",
        description: "Creates task in the database",
        OperationId = "createTask",
        Tags = new[] { "Task API" })]
    public async Task<IActionResult> CreateTask
        (QuestDto questDto, CancellationToken ct)
    {
        QuestDto dto = await _taskService.CreateAsync(questDto, ct);
        return StatusCode(StatusCodes.Status201Created, dto);
    }
}