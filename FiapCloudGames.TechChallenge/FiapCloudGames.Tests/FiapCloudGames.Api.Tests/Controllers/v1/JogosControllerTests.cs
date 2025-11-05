using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Api.Controllers.v1;
using FiapCloudGames.Api.Tests.Extensions;
using FiapCloudGames.Application.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json;

namespace FiapCloudGames.Api.Tests.Controllers.v1;

[ExcludeFromCodeCoverage]
public class JogosControllerTests
{
    private readonly JogosController _controller;

    private readonly Mock<IJogoAppService> _jogoServiceMock = new();
    private readonly Mock<IValidator<JogoDto>> _jogoValidatorMock = new();

    public JogosControllerTests()
    {
        _controller = new JogosController(_jogoServiceMock.Object, _jogoValidatorMock.Object);

        ClaimsPrincipal user = new(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, "Admin")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task GetAsync_ReturnsListaJogos()
    {
        List<JogoDto> jogos =
        [
            new(Guid.NewGuid(), "Forza Horizon", DateTimeExtensions.DataConvertida("23/10/2012"), 229.9m),
            new(Guid.NewGuid(), "Gran Turismo 7", DateTimeExtensions.DataConvertida("04/03/2022"), 349.9m)
        ];

        _jogoServiceMock.Setup(s => s.ObterJogosAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(jogos);

        var result = await _controller.GetAsync(CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<JogoDto>>(okResult.Value);

        Assert.Equal(2, ((List<JogoDto>)returnValue).Count);
    }

    [Fact]
    public async Task GetPorIdAsync_JogoNaoEncontrado_ReturnsNotFound()
    {
        _jogoServiceMock.Setup(s => s.ObterJogoPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((JogoDto)null!);

        var result = await _controller.GetPorIdAsync(Guid.NewGuid(), CancellationToken.None);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Contains("Não encontrado", notFoundResult.Value!.ToString());
    }

    [Fact]
    public async Task GetPorIdAsync_JogoEncontrado_ReturnsOk()
    {
        var id = Guid.NewGuid();
        var jogo = new JogoDto(id, "FIFA 13", DateTimeExtensions.DataConvertida("25/09/2012"), 179.9m);

        _jogoServiceMock.Setup(s => s.ObterJogoPorIdAsync(id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(jogo);

        var result = await _controller.GetPorIdAsync(id, CancellationToken.None);

        var contentResult = Assert.IsType<ContentResult>(result.Result);
        var returnValue = JsonSerializer.Deserialize<JogoDto>(
            contentResult.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.Equal(jogo.Id, returnValue!.Id);
    }

    [Fact]
    public async Task GetPorNomeParcialAsync_JogoNaoEncontrado_ReturnsNotFound()
    {
        _jogoServiceMock.Setup(s => s.ObterJogoPorNomeParcialAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((JogoDto)null!);

        var result = await _controller.GetPorNomeParcialAsync("NomeInexistente", CancellationToken.None);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Contains("Não encontrado", notFoundResult.Value!.ToString());
    }

    [Fact]
    public async Task GetPorNomeParcialAsync_JogoEncontrado_ReturnsOk()
    {
        var jogo = new JogoDto(Guid.NewGuid(), "Red Dead Redemption 2", DateTimeExtensions.DataConvertida("26/10/2018"), 349.9m);

        _jogoServiceMock.Setup(s => s.ObterJogoPorNomeParcialAsync(jogo.Nome!, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(jogo);

        var result = await _controller.GetPorNomeParcialAsync(jogo.Nome!, CancellationToken.None);

        var contentResult = Assert.IsType<ContentResult>(result.Result);
        var returnValue = JsonSerializer.Deserialize<JogoDto>(
            contentResult.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.Equal(jogo.Nome, returnValue!.Nome);
    }

    [Fact]
    public async Task GetPorAnoLancamentoAsync_JogoNaoEncontrado_ReturnsNotFound()
    {
        _jogoServiceMock.Setup(s => s.ObterJogosPorAnoLancamentoAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((IEnumerable<JogoDto>)null!);

        var result = await _controller.GetPorAnoLancamentoAsync(2025, CancellationToken.None);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Contains("Não encontrado", notFoundResult.Value!.ToString());
    }

    [Fact]
    public async Task GetPorAnoLancamentoAsync_JogoEncontrado_ReturnsOk()
    {
        var jogo = new JogoDto(Guid.NewGuid(), "Marvel's Spider-Man 2", DateTimeExtensions.DataConvertida("20/10/2023"), 399.9m);

        _jogoServiceMock.Setup(s => s.ObterJogosPorAnoLancamentoAsync(jogo.Lancamento!.Value.Year, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new List<JogoDto>() { jogo });

        var result = await _controller.GetPorAnoLancamentoAsync(jogo.Lancamento!.Value.Year, CancellationToken.None);

        var contentResult = Assert.IsType<ContentResult>(result.Result);

        var returnValue = JsonSerializer.Deserialize<List<JogoDto>>(
            contentResult.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.NotNull(returnValue);
        Assert.Contains(returnValue, j => j.Lancamento!.Value.Year == jogo.Lancamento!.Value.Year);
    }

    [Fact]
    public async Task CreateAsync_ReturnsCreated()
    {
        var jogoDto = new JogoDto(null, "Jogo novao em folha da massa", null, 99.9m);
        var jogoCriado = new JogoDto(Guid.NewGuid(), jogoDto.Nome, jogoDto.Lancamento, jogoDto.Preco);

        _jogoValidatorMock.Setup(v => v.ValidateAsync(It.IsAny<JogoDto>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());

        _jogoServiceMock.Setup(s => s.CriarJogoAsync(jogoDto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(jogoCriado);

        var result = await _controller.CreateAsync(jogoDto, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        var returnValue = Assert.IsType<JogoDto>(createdResult.Value);

        Assert.Equal(jogoCriado.Id, returnValue.Id);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsOk()
    {
        var id = Guid.NewGuid();
        var jogoDto = new JogoDto(id, "Jogao editado da massa", null, null);

        _jogoServiceMock.Setup(s => s.EditarJogoAsync(id, jogoDto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(jogoDto);

        var result = await _controller.UpdateAsync(id, jogoDto, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<JogoDto>(okResult.Value);

        Assert.Equal(jogoDto.Nome, returnValue.Nome);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsOk()
    {
        var id = Guid.NewGuid();

        _jogoServiceMock.Setup(s => s.DeletarJogoAsync(id, It.IsAny<CancellationToken>()))
                        .Returns(Task.CompletedTask);

        var result = await _controller.DeleteAsync(id, CancellationToken.None);

        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.Contains(id.ToString(), okResult.Value!.ToString());
    }
}