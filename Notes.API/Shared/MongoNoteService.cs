namespace Notes.API.Shared;

using MongoDB.Driver;

using Notes.API.Controllers;

public class MongoNoteService
{
    private readonly IMongoCollection<Note> _notes;

    public MongoNoteService(IMongoClient client)
    {
        var database = client.GetDatabase("mongodb");
        _notes = database.GetCollection<Note>("notes");
        if (_notes == null)
        {
            database.CreateCollection("notes");
            _notes = database.GetCollection<Note>("notes");
        }
    }

    // 🟢 Add a new note
    public async Task AddNoteAsync(Note note)
    {
        note.Id = Guid.NewGuid();
        await _notes.InsertOneAsync(note);
    }

    // 🔍 Get all notes filtered by category (optional)
    public async Task<List<NoteDto>> GetNotesAsync(string category = null)
    {
        var filter = Builders<Note>.Filter.Empty;

        if (!string.IsNullOrEmpty(category))
            filter &= Builders<Note>.Filter.Eq(n => n.Category, category);

        return await _notes.Find(filter)
                           .SortByDescending(n => n.CreatedAt)
                           .Project<NoteDto>(category == null ? Builders<Note>.Projection.Exclude(n => n.Category) : Builders<Note>.Projection.Expression(n => new NoteDto
                           {
                               Id = n.Id,
                               Title = n.Title,
                               Content = n.Content,
                               CreatedAt = n.CreatedAt,
                               UpdatedAt = n.UpdatedAt,
                               Category = n.Category
                           }))
                           .ToListAsync();
    }

    // 🗑 Delete a note
    public async Task DeleteNoteAsync(Guid noteId)
    {
        await _notes.DeleteOneAsync(n => n.Id == noteId);
    }

    // 📝 Update a note
    public async Task UpdateNoteAsync(Guid noteId, string newContent)
    {
        var update = Builders<Note>.Update.Set(n => n.Content, newContent);
        await _notes.UpdateOneAsync(n => n.Id == noteId, update);
    }
}