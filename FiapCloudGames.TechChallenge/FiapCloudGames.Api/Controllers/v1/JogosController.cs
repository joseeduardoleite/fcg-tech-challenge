using Asp.Versioning;
using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public sealed class JogosController(IJogoAppService jogoAppService) : FcgControllerBase
{
    /// <summary>
    /// Obtém todos os jogos (Admins - Todos, Usuários - Todos)
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Jogos retornados com sucesso</response>
    /// <response code="204">Lista de jogos vazia</response>
    /// <returns>Retorna uma lista de jogos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JogoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<JogoDto>>> GetAsync(CancellationToken cancellationToken)
        => ListResult(await jogoAppService.ObterJogosAsync(cancellationToken));

    /// <summary>
    /// Obtém um jogo por id (Admins - Todos, Usuários - Todos)
    /// </summary>
    /// <param name="id">Id do jogo</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Jogo retornado com sucesso</response>
    /// <response code="404">Jogo não encontrado</response>
    /// <returns>Retorna o jogo encontrado</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(JogoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JogoDto>> GetPorIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        => Result(await jogoAppService.ObterJogoPorIdAsync(id, cancellationToken));

    /// <summary>
    /// Obtém um jogo por nome parcial (Admins - Todos, Usuários - Todos)
    /// </summary>
    /// <param name="nome">Nome do jogo</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Jogo retornado com sucesso</response>
    /// <response code="404">Jogo não encontrado</response>
    /// <returns>Retorna o jogo encontrado</returns>
    [HttpGet("{nome}")]
    [ProducesResponseType(typeof(JogoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JogoDto>> GetPorNomeParcialAsync([FromRoute] string nome, CancellationToken cancellationToken)
        => Result(await jogoAppService.ObterJogoPorNomeParcialAsync(nome, cancellationToken));

    /// <summary>
    /// Obtém um jogo por ano de lançamento (Admins - Todos, Usuários - Todos)
    /// </summary>
    /// <param name="anoLancamento">Ano de lançamento do jogo</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Jogo retornado com sucesso</response>
    /// <response code="404">Jogo não encontrado</response>
    /// <returns>Retorna o jogo encontrado</returns>
    [HttpGet("{anoLancamento:int}")]
    [ProducesResponseType(typeof(JogoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JogoDto>> GetPorAnoLancamentoAsync([FromRoute] int anoLancamento, CancellationToken cancellationToken)
        => Result(await jogoAppService.ObterJogosPorAnoLancamentoAsync(anoLancamento, cancellationToken));

    /// <summary>
    /// Cria um jogo (Admins - Podem criar, Usuários - Sem permissão)
    /// </summary>
    /// <param name="jogoDto">Jogo a ser criado</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="201">Jogo criado com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns>Jogo criado</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(JogoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<JogoDto>> CreateAsync([FromBody] JogoDto jogoDto, CancellationToken cancellationToken)
    {
        JogoDto jogoCriado = await jogoAppService.CriarJogoAsync(jogoDto, cancellationToken);

        return Created(
            uri: $"v1/jogos/{jogoCriado.Id}",
            value: jogoCriado
        );
    }

    /// <summary>
    /// Atualiza um jogo (Admins - Todos, Usuários - Sem permissão)
    /// </summary>
    /// <param name="id">Id do jogo</param>
    /// <param name="jogoDto">Dados atualizados do jogo</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Jogo atualizado com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns>Jogo atualizado</returns>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(JogoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<JogoDto>> UpdateAsync([FromRoute] Guid id, [FromBody] JogoDto jogoDto, CancellationToken cancellationToken)
    {
        if (!IsOwnerOrAdmin(id))
            return Forbid();

        JogoDto jogoEditado = await jogoAppService.EditarJogoAsync(id, jogoDto, cancellationToken);

        return Ok(jogoEditado);
    }

    /// <summary>
    /// Deleta um jogo (Admins - Todos, Usuários - Sem permissão)
    /// </summary>
    /// <param name="id">Id do jogo</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <returns>Jogo deletado</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await jogoAppService.DeletarJogoAsync(id, cancellationToken);

        return Ok($"Jogo de id '{id}' deletado com sucesso!");
    }
}