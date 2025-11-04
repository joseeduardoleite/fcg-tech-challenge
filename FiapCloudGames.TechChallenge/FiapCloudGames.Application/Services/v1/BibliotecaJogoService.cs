using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories.v1;
using FiapCloudGames.Domain.Services.v1;

namespace FiapCloudGames.Application.Services.v1;

public sealed class BibliotecaJogoService(IBibliotecaJogoRepository bibliotecaJogoRepository) : IBibliotecaJogoService
{
    public async Task<IEnumerable<BibliotecaJogo>> ObterBibliotecasDeJogosAsync(CancellationToken cancellationToken)
        => await bibliotecaJogoRepository.ObterBibliotecasDeJogosAsync(cancellationToken);

    public async Task<BibliotecaJogo?> ObterBibliotecaDeJogoPorIdAsync(Guid id, CancellationToken cancellationToken)
        => await bibliotecaJogoRepository.ObterBibliotecaDeJogoPorIdAsync(id, cancellationToken);

    public async Task<BibliotecaJogo?> ObterBibliotecaDeJogosPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken)
        => await bibliotecaJogoRepository.ObterBibliotecaDeJogosPorUsuarioIdAsync(usuarioId, cancellationToken);

    public async Task<BibliotecaJogo> AdicionarJogoABibliotecaDeJogosAsync(Guid usuarioId, Jogo jogoDto, CancellationToken cancellationToken)
        => await bibliotecaJogoRepository.AdicionarJogoABibliotecaDeJogosAsync(usuarioId, jogoDto, cancellationToken);

    public async Task<BibliotecaJogo> RemoverJogoBibliotecaJogosAsync(Guid usuarioId, Guid idJogo, CancellationToken cancellationToken)
        => await bibliotecaJogoRepository.RemoverJogoBibliotecaJogosAsync(usuarioId, idJogo, cancellationToken);
}