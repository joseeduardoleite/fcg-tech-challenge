using Asp.Versioning;
using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Application.Dtos;
using FiapCloudGames.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
public sealed class UsuariosController(IUsuarioAppService usuarioAppService) : ControllerBase
{
    /// <summary>
    /// Obtém todos os usuários (Admins - Todos, Usuários - Sem acesso)
    /// </summary>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Usuários retornados com sucesso</response>
    /// <response code="204">Lista de usuários vazia</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns>Retorna uma lista de usuários</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAsync(CancellationToken cancellationToken)
    {
        IEnumerable<UsuarioDto> usuarios = await usuarioAppService.ObterUsuarioAsync(cancellationToken);

        return usuarios.Any() ? Ok(usuarios) : NoContent();
    }

    /// <summary>
    /// Obtém usuário por id (Admins - Todos, Usuários - Somente o próprio)
    /// </summary>
    /// <param name="id">Id do usuário</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Usuário retornado com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <returns>Retorna o usuário encontrado</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioDto>> GetPorIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        UsuarioDto usuario = await usuarioAppService.ObterUsuarioPorIdAsync(id, cancellationToken);

        string? role = User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
        string? userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (usuario is null)
            return NotFound();

        if (role != "Admin" && userId != usuario.Id.ToString())
            return Forbid();

        return Ok(usuario);
    }

    /// <summary>
    /// Obtém usuário por e-mail (Admins - Todos, Usuários - Somente o próprio)
    /// </summary>
    /// <param name="email">E-mail do usuário</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Usuário retornado com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <returns>Retorna o usuário encontrado</returns>
    [HttpGet("{email}")]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioDto>> GetPorEmailAsync([FromRoute] string email, CancellationToken cancellationToken)
    {
        UsuarioDto usuario = await usuarioAppService.ObterUsuarioPorEmailAsync(email, cancellationToken);

        string? role = User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
        string? userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (usuario is null)
            return NotFound();

        if (role != "Admin" && userId != usuario.Id.ToString())
            return Forbid();

        return Ok(usuario);
    }

    /// <summary>
    /// Cria um usuário (Admins - Podem criar, Usuários - Podem criar)
    /// </summary>
    /// <param name="usuarioDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<UsuarioDto>> CreateAsync([FromBody] UsuarioDto usuarioDto, CancellationToken cancellationToken)
    {
        UsuarioDto usuarioCriado = await usuarioAppService.CriarUsuarioAsync(usuarioDto, cancellationToken);

        return Created(
            uri: $"v1/usuarios/{usuarioCriado.Id}",
            value: usuarioCriado
        );
    }

    /// <summary>
    /// Atualiza um usuário (Admins - Todos, Usuários - Somente o próprio)
    /// </summary>
    /// <param name="id">Id do usuário</param>
    /// <param name="usuarioDto">Dados atualizados do usuário</param>
    /// <param name="cancellationToken">Token para cancelamento da requisição</param>
    /// <response code="200">Usuário atualizado com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns>Usuário atualizado</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UsuarioDto>> UpdateAsync([FromRoute] Guid id, [FromBody] UsuarioDto usuarioDto, CancellationToken cancellationToken)
    {
        string? role = User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
        string? userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (role != "Admin" && userId != id.ToString())
            return Forbid();

        UsuarioDto usuarioEditado = await usuarioAppService.EditarUsuarioAsync(id, usuarioDto, cancellationToken);

        return Ok(usuarioEditado);
    }

    /// <summary>
    /// Deleta um usuário
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Usuário deletado com sucesso</response>
    /// <response code="403">Privilégios insuficientes</response>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await usuarioAppService.DeletarUsuarioAsync(id, cancellationToken);

        return Ok($"Usuário de id '{id}' deletado com sucesso!");
    }
}