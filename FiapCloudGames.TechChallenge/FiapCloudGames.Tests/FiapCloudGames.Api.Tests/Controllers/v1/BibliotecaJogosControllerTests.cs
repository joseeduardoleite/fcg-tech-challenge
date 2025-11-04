using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Api.Controllers.v1;
using FiapCloudGames.Api.Tests.Extensions;
using FiapCloudGames.Application.Dtos;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Api.Tests.Controllers.v1;

[ExcludeFromCodeCoverage]
public class BibliotecaJogosControllerTests
{
    private readonly BibliotecaJogosController _controller;

    private readonly Mock<IBibliotecaJogoAppService> _bibliotecaJogoAppServiceMock = new();

    public BibliotecaJogosControllerTests()
        => _controller = new(_bibliotecaJogoAppServiceMock.Object);

    [Fact]
    public async Task GetAsync_Admin_ReturnsAllBibliotecas()
    {
        var bibliotecas = new List<BibliotecaJogoDto>
        {
            new(
                Id: Guid.NewGuid(),
                UsuarioId: Guid.NewGuid(),
                UsuarioNome: "Eduardo",
                Jogos: new List<JogoDto>()
            ),
            new(
                Id: Guid.NewGuid(),
                UsuarioId: Guid.NewGuid(),
                UsuarioNome: "Francisco",
                Jogos: new List<JogoDto>()
            )
        };

        _bibliotecaJogoAppServiceMock.Setup(x => x.ObterBibliotecasDeJogosAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(bibliotecas);

        var result = await _controller.GetAsync(CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetPorIdAsync_OwnerOrAdmin_ReturnsBiblioteca()
    {
        var id = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();

        var biblioteca = new BibliotecaJogoDto(id, usuarioId, "Eduardo", new List<JogoDto>());

        _bibliotecaJogoAppServiceMock.Setup(s => s.ObterBibliotecaDeJogoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(biblioteca);

        _controller.SetUserClaims(usuarioId, "Usuario");

        var result = await _controller.GetPorIdAsync(biblioteca.Id, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task AdicionarJogoAsync_Owner_ReturnsCreated()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();

        var jogo = new JogoDto(jogoId, "Jogo X", DateTime.UtcNow, 199.9m);
        var biblioteca = new BibliotecaJogoDto(Guid.NewGuid(), usuarioId, "Eduardo", new List<JogoDto> { jogo });

        _bibliotecaJogoAppServiceMock.Setup(s => s.AdicionarJogoABibliotecaDeJogosAsync(usuarioId, jogo, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(biblioteca);

        _controller.SetUserClaims(usuarioId, "Usuario");

        var result = await _controller.AdicionarJogoAsync(usuarioId, jogo, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task RemoverJogoBibliotecaAsync_Admin_ReturnsOk()
    {
        var usuarioId = Guid.NewGuid();
        var jogoId = Guid.NewGuid();

        var jogo = new JogoDto(jogoId, "Jogo X", DateTime.UtcNow, 199.9m);
        var biblioteca = new BibliotecaJogoDto(Guid.NewGuid(), usuarioId, "Eduardo", new List<JogoDto> { jogo });

        _bibliotecaJogoAppServiceMock.Setup(s => s.RemoverJogoBibliotecaJogosAsync(usuarioId, jogoId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(biblioteca);

        var result = await _controller.RemoverJogoBibliotecaAsync(usuarioId, jogoId, CancellationToken.None);

        Assert.NotNull(result);
    }
}