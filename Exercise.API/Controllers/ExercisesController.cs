namespace Exercises.API.Controllers;

using Exercises.Application.Common.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ExercisesController : ControllerBase
{
    private readonly ILogger<ExercisesController> _logger;
    private readonly IMediator _mediator;
    private readonly IExercisesDbContext _context;

    public ExercisesController(IExercisesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetExercises([FromQuery] PaginationParameters pp)
    {
        var exercises = await _context.Exercises
            .Skip(pp.PageSize * pp.StartIndex)
            .Take(pp.PageSize)
            .ToListAsync();

        return Ok(exercises);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExercise(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        return Ok(exercise);
    }
}