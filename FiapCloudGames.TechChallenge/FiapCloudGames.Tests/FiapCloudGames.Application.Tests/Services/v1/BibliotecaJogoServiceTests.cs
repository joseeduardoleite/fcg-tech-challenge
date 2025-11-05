using FiapCloudGames.Application.Services.v1;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories.v1;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application.Tests.Services.v1;

[ExcludeFromCodeCoverage]
public class BibliotecaJogoServiceTests
{
    private readonly BibliotecaJogoService _service;

    private readonly Mock<IBibliotecaJogoRepository> _bibliotecaRepoMock = new();

    public BibliotecaJogoServiceTests()
        => _service = new BibliotecaJogoService(_bibliotecaRepoMock.Object);

    [Fact]
    public async Task ObterBibliotecasDeJogosAsync_ReturnsLista()
    {
        var bibliotecas = new List<BibliotecaJogo>
        {
            new(Guid.NewGuid(), new List<Jogo>()),
            new(Guid.NewGuid(), new List<Jogo>())
        };

        _bibliotecaRepoMock
            .Setup(x => x.ObterBibliotecasDeJogosAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(bibliotecas);

        var result = await _service.ObterBibliotecasDeJogosAsync(CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ObterBibliotecaDeJogoPorIdAsync_ReturnsBiblioteca()
    {
        var id = Guid.NewGuid();
        var biblioteca = new BibliotecaJogo()
        {
            Id = id,
            UsuarioId = Guid.NewGuid(),
            Jogos = []
        };

        _bibliotecaRepoMock
            .Setup(x => x.ObterBibliotecaDeJogoPorIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(biblioteca);

        var result = await _service.ObterBibliotecaDeJogoPorIdAsync(id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
    }

    [Fact]
    public async Task ObterBibliotecaDeJogosPorUsuarioIdAsync_ReturnsBiblioteca()
    {
        var usuarioId = Guid.NewGuid();
        var biblioteca = new BibliotecaJogo(usuarioId, new List<Jogo>());

        _bibliotecaRepoMock
            .Setup(x => x.ObterBibliotecaDeJogosPorUsuarioIdAsync(usuarioId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(biblioteca);

        var result = await _service.ObterBibliotecaDeJogosPorUsuarioIdAsync(usuarioId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(usuarioId, result!.UsuarioId);
    }

    [Fact]
    public async Task AdicionarJogoABibliotecaDeJogosAsync_ReturnsBibliotecaAtualizada()
    {
        var usuarioId = Guid.NewGuid();
        var jogo = new Jogo { Id = Guid.NewGuid(), Nome = "Jogo Teste" };
        var bibliotecaAtualizada = new BibliotecaJogo(usuarioId, new List<Jogo> { jogo });

        _bibliotecaRepoMock
            .Setup(x => x.AdicionarJogoABibliotecaDeJogosAsync(usuarioId, jogo, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bibliotecaAtualizada);

        var result = await _service.AdicionarJogoABibliotecaDeJogosAsync(usuarioId, jogo, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Contains(jogo, result.Jogos);
        Assert.Equal(usuarioId, result.UsuarioId);
    }

    [Fact]
    public async Task RemoverJogoBibliotecaJogosAsync_ReturnsBibliotecaAtualizada()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();
        var biblioteca = new BibliotecaJogo(usuarioId, new List<Jogo> { });

        _bibliotecaRepoMock
            .Setup(x => x.RemoverJogoBibliotecaJogosAsync(usuarioId, jogoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(biblioteca);

        var result = await _service.RemoverJogoBibliotecaJogosAsync(usuarioId, jogoId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Jogos);
        Assert.Equal(usuarioId, result.UsuarioId);
    }
}