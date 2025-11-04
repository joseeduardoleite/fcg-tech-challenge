using FiapCloudGames.Application.Dtos;

namespace FiapCloudGames.Api.AppServices.v1.Interfaces;

public interface IJogoAppService
{
    Task<IEnumerable<JogoDto>> ObterJogosAsync(CancellationToken cancellationToken);
    Task<IEnumerable<JogoDto>> ObterJogosPorAnoLancamentoAsync(int anoLancamento, CancellationToken cancellationToken);
    Task<JogoDto> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<JogoDto> ObterJogoPorNomeParcialAsync(string nome, CancellationToken cancellationToken);
    Task<JogoDto> CriarJogoAsync(JogoDto jogoDto, CancellationToken cancellationToken);
    Task<JogoDto> EditarJogoAsync(Guid id, JogoDto jogoDto, CancellationToken cancellationToken);
    Task DeletarJogoAsync(Guid id, CancellationToken cancellationToken);
}