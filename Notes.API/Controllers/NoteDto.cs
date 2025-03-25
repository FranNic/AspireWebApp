namespace Notes.API.Controllers;

public class NoteDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Category { get; set; }
    public string[] Tags { get; set; }
}

public static class NoteDtoExtensions
{
    public static Note ToNote(this NoteDto noteDto)
    {
        return new Note
        {
            Id = noteDto.Id,
            Title = noteDto.Title,
            Content = noteDto.Content,
            CreatedAt = noteDto.CreatedAt,
            UpdatedAt = noteDto.UpdatedAt,
            Category = noteDto.Category,
            Tags = noteDto.Tags
        };
    }

    public static NoteDto ToNoteDto(this Note note)
    {
        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt,
            UpdatedAt = note.UpdatedAt,
            Category = note.Category,
            Tags = note.Tags
        };
    }
}