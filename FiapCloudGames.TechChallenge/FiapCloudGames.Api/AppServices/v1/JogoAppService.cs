using AutoMapper;
using FiapCloudGames.Api.AppServices.v1.Interfaces;
using FiapCloudGames.Application.Dtos;
using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Services.v1;

namespace FiapCloudGames.Api.AppServices.v1;

public sealed class JogoAppService(IJogoService jogoService, IMapper mapper) : IJogoAppService
{
    public async Task<IEnumerable<JogoDto>> ObterJogosAsync(CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<JogoDto>>(await jogoService.ObterJogosAsync(cancellationToken));

    public async Task<IEnumerable<JogoDto>> ObterJogosPorAnoLancamentoAsync(int anoLancamento, CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<JogoDto>>(await jogoService.ObterJogosPorAnoLancamentoAsync(anoLancamento, cancellationToken));

    public async Task<JogoDto> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken)
        => mapper.Map<JogoDto>(await jogoService.ObterJogoPorIdAsync(id, cancellationToken));

    public async Task<JogoDto> ObterJogoPorNomeParcialAsync(string nome, CancellationToken cancellationToken)
        => mapper.Map<JogoDto>(await jogoService.ObterJogoPorNomeParcialAsync(nome, cancellationToken));

    public async Task<JogoDto> CriarJogoAsync(JogoDto jogoDto, CancellationToken cancellationToken)
        => mapper.Map<JogoDto>(await jogoService.CriarJogoAsync(mapper.Map<Jogo>(jogoDto), cancellationToken));

    public async Task<JogoDto> EditarJogoAsync(Guid id, JogoDto jogoDto, CancellationToken cancellationToken)
        => mapper.Map<JogoDto>(await jogoService.EditarJogoAsync(id, mapper.Map<Jogo>(jogoDto), cancellationToken));

    public async Task DeletarJogoAsync(Guid id, CancellationToken cancellationToken)
        => await jogoService.DeletarJogoAsync(id, cancellationToken);
}