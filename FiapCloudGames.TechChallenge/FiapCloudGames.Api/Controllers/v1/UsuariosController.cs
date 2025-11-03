using Asp.Versioning;
using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Application.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(string), 200)]
public sealed class UsuariosController(IUsuarioAppService usuarioAppService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAsync(CancellationToken cancellationToken)
    {
        IEnumerable<UsuarioDto> usuarios = await usuarioAppService.ObterAsync(cancellationToken);

        return usuarios.Any() ? Ok(usuarios) : NoContent();
    }

    /// <summary>
    /// Obtém usuário por id
    /// </summary>
    /// <param name="id">Id do usuário</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Usuário retornado com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <returns>Retorna o usuário encontrado</returns>
    /*[Authorize(Roles = "Admin")]*/
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioDto>> GetPorIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        UsuarioDto usuario = await usuarioAppService.ObterPorIdAsync(id, cancellationToken);

        return usuario is not null ? Ok(usuario) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<UsuarioDto>> CreateAsync([FromBody] UsuarioDto usuarioDto, CancellationToken cancellationToken)
    {
        UsuarioDto usuarioCriado = await usuarioAppService.CriarUsuarioAsync(usuarioDto, cancellationToken);

        return CreatedAtAction(
            actionName: nameof(GetPorIdAsync),
            routeValues: new { id = usuarioCriado.Id },
            value: usuarioCriado
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UsuarioDto>> UpdateAsync([FromRoute] Guid id, [FromBody] UsuarioDto usuarioDto, CancellationToken cancellationToken)
    {
        UsuarioDto usuarioEditado = await usuarioAppService.EditarUsuarioAsync(id, usuarioDto, cancellationToken);

        return Ok(usuarioEditado);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await usuarioAppService.DeletarUsuarioAsync(id, cancellationToken);

        return Ok($"Usuário de id '{id}' deletado com sucesso!");
    }
}