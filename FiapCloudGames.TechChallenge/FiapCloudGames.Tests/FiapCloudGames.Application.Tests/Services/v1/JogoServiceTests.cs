using FiapCloudGames.Application.Services.v1;
using FiapCloudGames.Application.Tests.Extensions;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories.v1;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application.Tests.Services.v1;

[ExcludeFromCodeCoverage]
public class JogoServiceTests
{
    private readonly JogoService _jogoService;

    private readonly Mock<IJogoRepository> _jogoRepositoryMock = new();

    public JogoServiceTests()
        => _jogoService = new JogoService(_jogoRepositoryMock.Object);

    [Fact]
    public async Task ObterJogosAsync_ReturnsLista()
    {
        List<Jogo> jogos =
        [
            new(nome: "Forza Horizon", preco: 229.9m, lancamento: DateTimeExtensions.DataConvertida("23/10/2012")),
            new(nome: "Gran Turismo 7", preco: 349.9m, lancamento: DateTimeExtensions.DataConvertida("23/10/2012"))
        ];

        _jogoRepositoryMock
            .Setup(x => x.ObterJogosAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(jogos);

        var result = await _jogoService.ObterJogosAsync(CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ObterJogosPorAnoLancamentoAsync_ReturnsListaFiltrada()
    {
        var ano = 2012;
        List<Jogo> jogos =
        [
            new(nome: "Forza Horizon", preco: 229.9m, lancamento: DateTimeExtensions.DataConvertida("23/10/2012"))
        ];

        _jogoRepositoryMock
            .Setup(x => x.ObterJogosPorAnoLancamentoAsync(ano, It.IsAny<CancellationToken>()))
            .ReturnsAsync(jogos);

        var result = await _jogoService.ObterJogosPorAnoLancamentoAsync(ano, CancellationToken.None);

        Assert.Single(result);
        Assert.Equal(ano, result.First().Lancamento!.Value.Year);
    }

    [Fact]
    public async Task ObterJogoPorIdAsync_ReturnsJogo()
    {
        var id = Guid.NewGuid();
        var jogo = new Jogo { Id = id, Nome = "Jogo teste duzera nos finalmentes pra entregar" };

        _jogoRepositoryMock
            .Setup(x => x.ObterJogoPorIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(jogo);

        var result = await _jogoService.ObterJogoPorIdAsync(id, CancellationToken.None);
                Assert.NotNull(result);

        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public async Task ObterJogoPorNomeParcialAsync_ReturnsJogo()
    {
        var nome = "Jogo final";
        var jogo = new Jogo { Id = Guid.NewGuid(), Nome = nome };

        _jogoRepositoryMock
            .Setup(x => x.ObterJogoPorNomeParcialAsync(nome, It.IsAny<CancellationToken>()))
            .ReturnsAsync(jogo);

        var result = await _jogoService.ObterJogoPorNomeParcialAsync(nome, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(nome, result!.Nome);
    }

    [Fact]
    public async Task CriarJogoAsync_ReturnsJogoCriado()
    {
        var jogo = new Jogo { Id = Guid.NewGuid(), Nome = "Novo Jogo" };

        _jogoRepositoryMock
            .Setup(x => x.CriarJogoAsync(jogo, It.IsAny<CancellationToken>()))
            .ReturnsAsync(jogo);

        var result = await _jogoService.CriarJogoAsync(jogo, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(jogo.Id, result.Id);
    }

    [Fact]
    public async Task EditarJogoAsync_ReturnsJogoEditado()
    {
        var id = Guid.NewGuid();
        var jogo = new Jogo { Id = id, Nome = "Editar Jogo" };

        _jogoRepositoryMock
            .Setup(x => x.EditarJogoAsync(id, jogo, It.IsAny<CancellationToken>()))
            .ReturnsAsync(jogo);

        var result = await _jogoService.EditarJogoAsync(id, jogo, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public async Task DeletarJogoAsync_ChamaRepositorio()
    {
        var id = Guid.NewGuid();

        _jogoRepositoryMock
            .Setup(x => x.DeletarJogoAsync(id, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        await _jogoService.DeletarJogoAsync(id, CancellationToken.None);

        _jogoRepositoryMock.Verify(x => x.DeletarJogoAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}
