using FiapCloudGames.Domain.Entities;

namespace FiapCloudGames.Domain.Repositories.v1;

public interface IJogoRepository
{
    Task<IEnumerable<Jogo>> ObterJogosAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Jogo>> ObterJogosPorAnoLancamentoAsync(int anoLancamento, CancellationToken cancellationToken);
    Task<Jogo?> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Jogo?> ObterJogoPorNomeParcialAsync(string nome, CancellationToken cancellationToken);
    Task<Jogo> CriarJogoAsync(Jogo jogo, CancellationToken cancellationToken);
    Task<Jogo?> EditarJogoAsync(Guid id, Jogo jogo, CancellationToken cancellationToken);
    Task DeletarJogoAsync(Guid id, CancellationToken cancellationToken);
}