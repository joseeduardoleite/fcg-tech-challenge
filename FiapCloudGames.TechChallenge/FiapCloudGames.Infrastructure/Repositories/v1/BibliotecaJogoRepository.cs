using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories.v1;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Infrastructure.Repositories.v1;

[ExcludeFromCodeCoverage]
public sealed class BibliotecaJogoRepository : IBibliotecaJogoRepository
{
    private static readonly List<BibliotecaJogo> _bibliotecas = [];

    public static void Seed(IEnumerable<Usuario> usuarios)
    {
        if (_bibliotecas.Count != 0)
            return;

        foreach (var user in usuarios)
            _bibliotecas.Add(new BibliotecaJogo
            {
                Id = Guid.NewGuid(),
                UsuarioId = user.Id,
                Usuario = user,
                Jogos = new List<Jogo>()
            });
    }
    public async Task<IEnumerable<BibliotecaJogo>> ObterBibliotecasDeJogosAsync(CancellationToken cancellationToken)
        => await Task.FromResult(_bibliotecas);

    public async Task<BibliotecaJogo?> ObterBibliotecaDeJogoPorIdAsync(Guid id, CancellationToken cancellationToken)
        => await Task.FromResult(_bibliotecas.FirstOrDefault(biblioteca => biblioteca.Id == id));

    public async Task<BibliotecaJogo?> ObterBibliotecaDeJogosPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken)
        => await Task.FromResult(_bibliotecas.FirstOrDefault(biblioteca => biblioteca.UsuarioId == usuarioId));

    public async Task<BibliotecaJogo> AdicionarJogoABibliotecaDeJogosAsync(Guid usuarioId, Jogo jogoDto, CancellationToken cancellationToken)
    {
        BibliotecaJogo? biblioteca = _bibliotecas.FirstOrDefault(b => b.UsuarioId == usuarioId);

        if (biblioteca is null)
        {
            biblioteca = new BibliotecaJogo
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuarioId,
                Jogos = new List<Jogo>()
            };

            _bibliotecas.Add(biblioteca);
        }

        if (biblioteca.Jogos.Any(jogo => jogo.Id == jogoDto.Id))
            throw new InvalidOperationException("Jogo já existe na biblioteca");

        biblioteca.Jogos.Add(jogoDto);

        return await Task.FromResult(biblioteca);

    }

    public async Task<BibliotecaJogo> RemoverJogoBibliotecaJogosAsync(Guid usuarioId, Guid jogoId, CancellationToken cancellationToken)
    {
        BibliotecaJogo biblioteca = _bibliotecas.FirstOrDefault(biblioteca => biblioteca.UsuarioId == usuarioId)
            ?? throw new KeyNotFoundException("Biblioteca não encontrada");

        Jogo jogo = biblioteca.Jogos.FirstOrDefault(jogo => jogo.Id == jogoId)
            ?? throw new KeyNotFoundException("Jogo não encontrado na biblioteca");

        biblioteca.Jogos.Remove(jogo);

        return await Task.FromResult(biblioteca);
    }
}