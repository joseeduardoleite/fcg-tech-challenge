using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Repositories.v1;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ObterUsuariosAsync(CancellationToken cancellationToken);
    Task<Usuario?> ObterUsuarioPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Usuario> CriarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken);
    Task<Usuario> EditarUsuarioAsync(Guid id, Usuario usuario, CancellationToken cancellationToken);
    Task DeletarUsuarioAsync(Guid id, CancellationToken cancellationToken);
    Task<Usuario?> ObterUsuarioPorEmailAsync(string email, CancellationToken cancellationToken);
}