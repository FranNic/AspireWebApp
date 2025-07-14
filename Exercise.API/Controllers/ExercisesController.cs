using AspireWebApp.Shared;

namespace Exercises.API.Controllers;

using Exercises.Application.Common.Interfaces;
using Exercises.Application.DTOs;
using Exercises.Application.Mappers;
using Exercises.Domain;

using MediatR;

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
    public async Task<ActionResult<PaginatedList<ExerciseDto>>> GetExercises([FromQuery] PaginationParameters pp)
    {
        var exercises = await _context.Exercises.AsNoTracking()
            .Search(pp.SearchValue!)
            .PaginatedListAsync(pp.StartIndex, pp.PageSize, Exercise => Exercise.ToDto());

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