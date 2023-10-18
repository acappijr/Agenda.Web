using Agenda.Domain.Ceps;

namespace Agenda.Infrastructure.RestResources;
public interface ICepExternalService
{
    Task<CepResponse?> GetAsync(string codigo);
}