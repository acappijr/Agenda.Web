using Agenda.Application.Services;
using Agenda.Domain.Ceps;
using Agenda.Domain.Contatos;
using NSubstitute;

namespace Agenda.Application.Tests;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
public class ContatoServiceTests
{
    private readonly ContatoService _sut;
    private readonly ICepRepository _cepRepositoryMock = Substitute.For<ICepRepository>();
    private readonly IContatoRepository _contatoRepositoryMock = Substitute.For<IContatoRepository>();
    private readonly Contato _contatoValido;
    private readonly Cep _cepValido;

    public ContatoServiceTests()
    {
        _sut = new ContatoService(_contatoRepositoryMock, _cepRepositoryMock);
        _cepValido = new Cep(Guid.NewGuid(), "01330000", "SP", "São Paulo", "Bela Vista", "Rua Rocha");
        _contatoValido = new Contato(0, "João da Silva", "119875654321", "emailjoao@teste.com", _cepValido.Id, "113", "");
        _contatoValido.Cep = _cepValido;
    }

    [Fact]
    public async Task Contato_CadastrarContato_DeveAcionarRepositoriosCepEContatoTest()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);

        // Act
        await _sut.CadastrarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(1).InserirCepAsync(Arg.Any<Cep>());
        await _contatoRepositoryMock.Received(1).InserirContatoAsync(Arg.Any<Contato>());
    }

    [Fact]
    public async Task Contato_CadastrarContato_DeveAcionarSomenteRepositorioContatoTest()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);
        _cepRepositoryMock.ObterCepPorCodigoAsync(contatoViewModel.Cep.Codigo).Returns(_cepValido);

        // Act
        await _sut.CadastrarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(0).InserirCepAsync(Arg.Any<Cep>());
        await _contatoRepositoryMock.Received(1).InserirContatoAsync(Arg.Any<Contato>());
    }

    [Fact]
    public async Task Contato_CadastrarContato_DeveGerarNovoCepId()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);

        // Act
        await _sut.CadastrarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(1).InserirCepAsync(Arg.Is<Cep>(c => c.Id != _cepValido.Id));
        await _contatoRepositoryMock.Received(1).InserirContatoAsync(Arg.Is<Contato>(c => c.CepId != _contatoValido.CepId));
    }

    [Fact]
    public async Task Contato_CadastrarContato_DeveManterCepId()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);
        _cepRepositoryMock.ObterCepPorCodigoAsync(contatoViewModel.Cep.Codigo).Returns(_cepValido);

        // Act
        await _sut.CadastrarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(0).InserirCepAsync(Arg.Any<Cep>());
        await _contatoRepositoryMock.Received(1).InserirContatoAsync(Arg.Is<Contato>(c => c.CepId == _contatoValido.CepId));
    }

    [Fact]
    public async Task Contato_AlterarContato_DeveAcionarRepositoriosCepEContatoTest()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);

        // Act
        await _sut.AlterarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(1).InserirCepAsync(Arg.Any<Cep>());
        await _contatoRepositoryMock.Received(1).AlterarContatoAsync(Arg.Any<Contato>());
    }

    [Fact]
    public async Task Contato_AlterarContato_DeveAcionarSomenteRepositorioContatoTest()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);
        _cepRepositoryMock.ObterCepPorCodigoAsync(contatoViewModel.Cep.Codigo).Returns(_cepValido);

        // Act
        await _sut.AlterarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(0).InserirCepAsync(Arg.Any<Cep>());
        await _contatoRepositoryMock.Received(1).AlterarContatoAsync(Arg.Any<Contato>());
    }

    [Fact]
    public async Task Contato_AlterarContato_DeveGerarNovoCepId()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);

        // Act
        await _sut.AlterarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(1).InserirCepAsync(Arg.Is<Cep>(c => c.Id != _cepValido.Id));
        await _contatoRepositoryMock.Received(1).AlterarContatoAsync(Arg.Is<Contato>(c => c.CepId != _contatoValido.CepId));
    }

    [Fact]
    public async Task Contato_AlterarContato_DeveManterCepId()
    {
        // Arrange
        var contatoViewModel = ContatoViewModel.MapearContatoParaViewModel(_contatoValido);
        _cepRepositoryMock.ObterCepPorCodigoAsync(contatoViewModel.Cep.Codigo).Returns(_cepValido);

        // Act
        await _sut.AlterarContatoAsync(contatoViewModel);

        // Assert
        await _cepRepositoryMock.Received(0).InserirCepAsync(Arg.Any<Cep>());
        await _contatoRepositoryMock.Received(1).AlterarContatoAsync(Arg.Is<Contato>(c => c.CepId == _contatoValido.CepId));
    }
}