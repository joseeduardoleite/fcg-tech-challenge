using AutoMapper;
using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Application.Dtos;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Services.v1;

namespace FiapCloudGames.Api.AppServices.v1;

public sealed class UsuarioAppService(IUsuarioService usuarioService, IMapper mapper) : IUsuarioAppService
{
    public async Task<UsuarioDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        => mapper.Map<UsuarioDto>(await usuarioService.ObterUsuarioPorIdAsync(id, cancellationToken));

    public async Task<IEnumerable<UsuarioDto>> ObterAsync(CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<UsuarioDto>>(await usuarioService.ObterUsuariosAsync(cancellationToken));

    public async Task<UsuarioDto> CriarUsuarioAsync(UsuarioDto usuarioDto, CancellationToken cancellationToken)
        => mapper.Map<UsuarioDto>(await usuarioService.CriarUsuarioAsync(mapper.Map<Usuario>(usuarioDto), cancellationToken));

    public async Task<UsuarioDto> EditarUsuarioAsync(Guid id, UsuarioDto usuarioDto, CancellationToken cancellationToken)
        => mapper.Map<UsuarioDto>(await usuarioService.EditarUsuarioAsync(id, mapper.Map<Usuario>(usuarioDto), cancellationToken));

    public async Task DeletarUsuarioAsync(Guid id, CancellationToken cancellationToken)
        => await usuarioService.DeletarUsuarioAsync(id, cancellationToken);
}