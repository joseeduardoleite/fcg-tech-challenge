using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories.v1;
using FiapCloudGames.Domain.Services.v1;

namespace FiapCloudGames.Application.Services.v1;

public sealed class JogoService(IJogoRepository jogoRepository) : IJogoService
{
    public async Task<IEnumerable<Jogo>> ObterJogosAsync(CancellationToken cancellationToken)
        => await jogoRepository.ObterJogosAsync(cancellationToken);

    public async Task<IEnumerable<Jogo>> ObterJogosPorAnoLancamentoAsync(int anoLancamento, CancellationToken cancellationToken)
        => await jogoRepository.ObterJogosPorAnoLancamentoAsync(anoLancamento, cancellationToken);

    public async Task<Jogo?> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken)
        => await jogoRepository.ObterJogoPorIdAsync(id, cancellationToken);

    public async Task<Jogo?> ObterJogoPorNomeParcialAsync(string nome, CancellationToken cancellationToken)
        => await jogoRepository.ObterJogoPorNomeParcialAsync(nome, cancellationToken);

    public async Task<Jogo> CriarJogoAsync(Jogo jogo, CancellationToken cancellationToken)
        => await jogoRepository.CriarJogoAsync(jogo, cancellationToken);

    public async Task<Jogo?> EditarJogoAsync(Guid id, Jogo jogo, CancellationToken cancellationToken)
        => await jogoRepository.EditarJogoAsync(id, jogo, cancellationToken);

    public async Task DeletarJogoAsync(Guid id, CancellationToken cancellationToken)
        => await jogoRepository.DeletarJogoAsync(id, cancellationToken);
}