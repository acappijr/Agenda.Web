namespace Agenda.Domain.Ceps;
public interface ICepRepository
{
    Task InserirCepAsync(Cep cep);
}
