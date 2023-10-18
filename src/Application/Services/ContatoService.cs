using Agenda.Domain.Ceps;
using Agenda.Domain.Contatos;

namespace Agenda.Application.Services;
public class ContatoService : IContatoService
{
    private readonly IContatoRepository _contatoRepository;
    private readonly ICepRepository _cepRepository;

    public ContatoService(IContatoRepository contatoRepository,
        ICepRepository cepRepository)
    {
        _contatoRepository = contatoRepository;
        _cepRepository = cepRepository;
    }

    public async Task CadastrarContatoAsync(ContatoViewModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var (contatoModel, cepModel) = model.ObterEntidadeContatoCep();

        var cepEntity = await _cepRepository.ObterCepPorCodigoAsync(cepModel.Codigo);

        if (cepEntity is null)
        {
            var guid = Guid.NewGuid();
            cepModel.Id = guid;
            contatoModel.CepId = guid;
            await _cepRepository.InserirCepAsync(cepModel);
        }
        else
        {
            contatoModel.CepId = cepEntity.Id;
        }

        await _contatoRepository.InserirContatoAsync(contatoModel);
    }

    public async Task<IEnumerable<ContatoViewModel>> ObterContatosComCepAsync()
    {
        var contatos = await _contatoRepository.ObterContatosComCepAsync();
        var contatosViewModel = ContatoViewModel.MapearContatoParaViewModel(contatos);

        return contatosViewModel;
    }

    public async Task<ContatoViewModel?> ObterContatoComCepAsync(int id)
    {
        var contato = await _contatoRepository.ObterContatoComCepAsync(id);

        if (contato is null)
            return null;

        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(contato);

        return contatoViewModel;
    }

    public async Task AlterarContatoAsync(ContatoViewModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var (contatoModel, cepModel) = model.ObterEntidadeContatoCep();

        var cepEntity = await _cepRepository.ObterCepPorCodigoAsync(cepModel.Codigo);

        if (cepEntity is null)
        {
            var guid = Guid.NewGuid();
            cepModel.Id = guid;
            contatoModel.CepId = guid;
            await _cepRepository.InserirCepAsync(cepModel);
        }
        else
        {
            contatoModel.CepId = cepEntity.Id;
        }

        await _contatoRepository.AlterarContatoAsync(contatoModel);
    }

    public async Task DeletarContatoAsync(int id)
    {
        await _contatoRepository.DeletarContatoAsync(id);

    }
}
