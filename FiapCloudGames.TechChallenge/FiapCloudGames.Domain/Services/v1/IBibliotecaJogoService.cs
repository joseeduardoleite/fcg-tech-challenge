using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Services.v1;

public interface IBibliotecaJogoService
{
    Task<IEnumerable<BibliotecaJogo>> ObterBibliotecasDeJogosAsync(CancellationToken cancellationToken);
    Task<BibliotecaJogo?> ObterBibliotecaDeJogoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BibliotecaJogo?> ObterBibliotecaDeJogosPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken);
    Task<BibliotecaJogo> AdicionarJogoABibliotecaDeJogosAsync(Guid usuarioId, Jogo jogoDto, CancellationToken cancellationToken);
    Task<BibliotecaJogo> RemoverJogoBibliotecaJogosAsync(Guid usuarioId, Guid idJogo, CancellationToken cancellationToken);
}