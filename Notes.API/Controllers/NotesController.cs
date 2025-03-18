using Microsoft.AspNetCore.Mvc;

using Notes.API.Shared;

namespace Notes.API.Controllers;

[ApiController]
[Route("[controller]")]
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
    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await _noteService.GetNotesAsync();
    }

    [HttpPost(Name = "AddNote")]
    public async Task<IActionResult> AddNoteAsync(Note note)
    {
        await _noteService.AddNoteAsync(note);
        return CreatedAtRoute("GetNotes", null);
    }
}
