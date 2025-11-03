using FiapCloudGames.Application.Dtos;

namespace FiapCloudGames.Api.AppServices.v1.Interfaces;

public interface IUsuarioAppService
{
    Task<IEnumerable<UsuarioDto>> ObterAsync(CancellationToken cancellationToken);
    Task<UsuarioDto> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<UsuarioDto> CriarUsuarioAsync(UsuarioDto usuarioDto, CancellationToken cancellationToken);
    Task<UsuarioDto> EditarUsuarioAsync(Guid id, UsuarioDto usuarioDto, CancellationToken cancellationToken);
    Task DeletarUsuarioAsync(Guid id, CancellationToken cancellationToken);
}