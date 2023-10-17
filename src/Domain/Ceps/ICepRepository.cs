namespace Agenda.Domain.Ceps;
public interface ICepRepository
{
    Task InserirCep(Cep cep);
}
