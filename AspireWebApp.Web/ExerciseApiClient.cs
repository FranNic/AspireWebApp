namespace AspireWebApp.Web;

using Shared;
using Exercises.Application.DTOs;
using Exercises.Domain;

public class ExerciseApiClient(HttpClient httpClient, ILogger<ExerciseApiClient> logger)
{
    public async Task<PaginatedList<ExerciseDto>> GetExercisesAsync(PaginationParameters pp, CancellationToken cancellationToken = default)
    {
        // add logging
        logger.LogInformation("Fetching exercises with parameters: PageNumber={PageNumber}, PageSize={PageSize}, SearchValue={SearchValue}",
            pp?.Page, pp?.PageSize, pp?.SearchValue);

        return await httpClient.GetFromJsonAsync<PaginatedList<ExerciseDto>>($"api/Exercises?pageNumber={pp?.Page}&pageSize={pp?.PageSize}&searchValue={pp?.SearchValue}", cancellationToken);
    }
}
