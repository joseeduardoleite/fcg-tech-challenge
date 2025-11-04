using Asp.Versioning;
using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers.v1;

/// <summary>
/// API responsável pelo controle da biblioteca de jogos dos usuários
/// </summary>
/// <param name="bibliotecaJogoAppService">Serviço de aplicação de biblioteca de jogos</param>
[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
public sealed class BibliotecaJogosController(IBibliotecaJogoAppService bibliotecaJogoAppService) : FcgControllerBase
{
    /// <summary>
    /// Obtém todas as bibliotecas de jogos dos usuários (Admins - Todos, Usuários - Sem acesso)
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Bibliotecas de jogos retornadas com sucesso</response>
    /// <response code="204">Lista de biblioteca de jogos vazia</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns>Retorna uma lista de bibliotecas de jogos dos usuários</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<BibliotecaJogoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<BibliotecaJogoDto>>> GetAsync(CancellationToken cancellationToken)
        => ListResult(await bibliotecaJogoAppService.ObterBibliotecasDeJogosAsync(cancellationToken));

    /// <summary>
    /// Obtém biblioteca de jogos por id (Admins - Todos, Usuários - Somente o próprio)
    /// </summary>
    /// <param name="id">Id da biblioteca de jogos</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Biblioteca de jogos retornada com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <response code="404">Biblioteca de jogos não encontrada</response>
    /// <returns>Retorna a biblioteca de jogos</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BibliotecaJogoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BibliotecaJogoDto>> GetPorIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        BibliotecaJogoDto bibliotecaDeJogos = await bibliotecaJogoAppService.ObterBibliotecaDeJogoPorIdAsync(id, cancellationToken);

        if (bibliotecaDeJogos is null)
            return NotFound();

        if (!IsOwnerOrAdmin(bibliotecaDeJogos.UsuarioId))
            return Forbid();

        return Ok(bibliotecaDeJogos);
    }

    /// <summary>
    /// Obtém biblioteca de jogos por id de usuario (Admins - Todos, Usuários - Somente o próprio)
    /// </summary>
    /// <param name="usuarioId">Id do usuário da biblioteca de jogos</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Biblioteca de jogos retornada com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <response code="404">Biblioteca de jogos não encontrada</response>
    /// <returns>Retorna a biblioteca de jogos</returns>
    [HttpGet("usuario/{usuarioId:guid}")]
    [ProducesResponseType(typeof(BibliotecaJogoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BibliotecaJogoDto>> GetPorUsuarioIdAsync([FromRoute] Guid usuarioId, CancellationToken cancellationToken)
    {
        BibliotecaJogoDto bibliotecaDeJogos = await bibliotecaJogoAppService.ObterBibliotecaDeJogosPorUsuarioIdAsync(usuarioId, cancellationToken);

        if (bibliotecaDeJogos is null)
            return NotFound();

        if (!IsOwnerOrAdmin(bibliotecaDeJogos.UsuarioId))
            return Forbid();

        return Ok(bibliotecaDeJogos);
    }

    /// <summary>
    /// Adiciona um jogo à biblioteca de jogos (Admins - Podem adicionar, Usuários - Somente em sua própria biblioteca)
    /// </summary>
    /// <param name="usuarioId">Id do usuário</param>
    /// <param name="jogoDto">Jogo à ser adicionado</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="201">Jogo adicionado à biblioteca com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns>Jogo adicionado à biblioteca de jogos</returns>
    [HttpPost("{usuarioId:guid}")]
    [ProducesResponseType(typeof(BibliotecaJogoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<BibliotecaJogoDto>> AdicionarJogoAsync([FromRoute] Guid usuarioId, [FromBody] JogoDto jogoDto, CancellationToken cancellationToken)
    {
        BibliotecaJogoDto bibliotecaDeJogos = await bibliotecaJogoAppService.AdicionarJogoABibliotecaDeJogosAsync(usuarioId, jogoDto, cancellationToken);

        if (!IsOwnerOrAdmin(bibliotecaDeJogos.UsuarioId))
            return Forbid();

        return Created(
            uri: $"v1/biblioteca-jogos",
            value: bibliotecaDeJogos
        );
    }

    /// <summary>
    /// Remove um jogo da biblioteca de jogos (Admins - Podem remover, Usuários - Sem acesso)
    /// </summary>
    /// <param name="usuarioId">Id do usuário</param>
    /// <param name="jogoId">Id do jogo</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Jogo removido da biblioteca com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns>Jogo removido</returns>
    [HttpDelete("{usuarioId:guid}/jogos/{jogoId:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> RemoverJogoBibliotecaAsync([FromRoute] Guid usuarioId, [FromRoute] Guid jogoId, CancellationToken cancellationToken)
    {
        await bibliotecaJogoAppService.RemoverJogoBibliotecaJogosAsync(usuarioId, jogoId, cancellationToken);

        return Ok($"Jogo removido com sucesso!");
    }
}