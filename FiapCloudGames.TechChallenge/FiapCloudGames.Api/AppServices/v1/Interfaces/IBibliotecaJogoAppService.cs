using FiapCloudGames.Application.Dtos;

namespace FiapCloudGames.Api.AppServices.v1.Interfaces;

public interface IBibliotecaJogoAppService
{
    Task<IEnumerable<BibliotecaJogoDto>> ObterBibliotecasDeJogosAsync(CancellationToken cancellationToken);
    Task<BibliotecaJogoDto> ObterBibliotecaDeJogoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BibliotecaJogoDto> ObterBibliotecaDeJogosPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken);
    Task<BibliotecaJogoDto> AdicionarJogoABibliotecaDeJogosAsync(Guid usuarioId, JogoDto jogoDto, CancellationToken cancellationToken);
    Task<BibliotecaJogoDto> RemoverJogoBibliotecaJogosAsync(Guid usuarioId, Guid idJogo, CancellationToken cancellationToken);
}