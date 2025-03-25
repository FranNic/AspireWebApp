namespace AspireWebApp.Web;

public class NotesApiClient(HttpClient httpClient, ILogger<NotesApiClient> logger)
{
    public async Task<List<NoteDto>> GetNotesAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<List<NoteDto>>("api/notes", cancellationToken);
    }

    public async Task<NoteDto> GetNoteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<NoteDto>($"api/notes/{id}", cancellationToken);
    }

    public async Task<NoteDto?> CreateNoteAsync(NoteDto note, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.PostAsJsonAsync("api/notes", note, cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError($"Failed to create note. Reason: {result.Content}");
                return null;
            }

            NoteDto? createdNote = await result.Content.ReadFromJsonAsync<NoteDto>(cancellationToken: cancellationToken);

            if (createdNote == null)
            {
                logger.LogWarning("Received null response when creating a note.");
            }

            return createdNote;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<NoteDto?> UpdateNoteAsync(Guid id, NoteDto note, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.PutAsJsonAsync($"api/notes/{id}", note, cancellationToken);
            if (!result.IsSuccessStatusCode)
            {
                logger.LogError("Failed to update note {id}", id);
                return null;
            }

            NoteDto? updatedNote = await result.Content.ReadFromJsonAsync<NoteDto>(cancellationToken: cancellationToken);

            if (updatedNote == null)
            {
                logger.LogWarning("Received null response when updating a note.");
            }

            return updatedNote;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> DeleteNoteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage result = await httpClient.DeleteAsync($"api/notes/{id}", cancellationToken);
            return result.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}