using Agenda.Domain.Ceps;
using System.Text.Json;

namespace Agenda.Infrastructure.RestResources;
public class CepExternalService : ICepExternalService
{
    private readonly HttpClient _httpClient;

    public CepExternalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CepResponse?> GetAsync(string codigo)
    {
#pragma warning disable CA2234 // Pass system uri objects instead of strings
        using var response = await _httpClient.GetAsync($"file/apicep/{codigo}.json");
#pragma warning restore CA2234 // Pass system uri objects instead of strings
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var cepResponse = JsonSerializer.Deserialize<CepResponse>(content);

            return cepResponse;
        }

        return null;
    }
}
