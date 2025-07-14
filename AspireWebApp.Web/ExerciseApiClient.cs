namespace AspireWebApp.Web;

using Shared;
using Exercises.Application.DTOs;
using Exercises.Domain;

public class ExerciseApiClient(HttpClient httpClient,ILogger<ExerciseApiClient> logger)
{
    public async Task<PaginatedList<ExerciseDto>> GetExercisesAsync(PaginationParameters pp, CancellationToken cancellationToken = default)
    {
        // add error management
        var start = pp.StartIndex;
        var page = pp.PageSize;
        var search = pp.SearchValue;
        
        return await httpClient.GetFromJsonAsync<PaginatedList<ExerciseDto>>($"api/Exercises?startIndex={pp?.StartIndex}&pageSize={pp?.PageSize}&searchValue={pp?.SearchValue}", cancellationToken);
    }
}
