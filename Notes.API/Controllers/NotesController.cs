using Microsoft.AspNetCore.Mvc;

using Notes.API.Shared;

namespace Notes.API.Controllers;

[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    private readonly ILogger<NotesController> _logger;
    private readonly MongoNoteService _noteService;

    public NotesController(ILogger<NotesController> logger, MongoNoteService noteService)
    {
        _logger = logger;
        _noteService = noteService;
    }

    [HttpGet(Name = "GetNotes")]
    public async Task<IEnumerable<NoteDto>> GetAllAsync()
    {
        return await _noteService.GetNotesAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddNoteAsync(NoteDto dto, CancellationToken cancellationToken)
    {
        await _noteService.AddNoteAsync(dto.ToNote());
        return CreatedAtRoute("GetNotes", dto);
    }
}
