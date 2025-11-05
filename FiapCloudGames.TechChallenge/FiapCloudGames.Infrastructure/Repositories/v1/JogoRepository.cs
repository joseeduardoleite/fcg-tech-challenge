using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories.v1;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace FiapCloudGames.Infrastructure.Repositories.v1;

[ExcludeFromCodeCoverage]
public sealed class JogoRepository : IJogoRepository
{
    private static readonly List<Jogo> _jogos = [];

    public static void Seed()
    {
        if (_jogos.Count != 0)
            return;

        _jogos.AddRange(new List<Jogo>()
        {
            new(
                nome: "Midnight Club 3: DUB Edition Remix",
                preco: 349.9m,
                lancamento: Data("11/04/2005")
            ),
            new(
                nome: "Grand Theft Auto: San Andreas",
                preco: 379.9m,
                lancamento: Data("26/10/2004")
            ),
            new(
                nome: "Need For Speed: Underground 2",
                preco: 329.9m,
                lancamento: Data("09/11/2004")
            ),
            new(
                nome: "Need For Speed: Underground",
                preco: 279.9m,
                lancamento: Data("03/10/2003")
            ),
            new(
                nome: "Grand Theft Auto IV",
                preco: 399.9m,
                lancamento: Data("29/04/2008")
            ),
            new(
                nome: "Grand Theft Auto V",
                preco: 499.9m,
                lancamento: Data("17/09/2013")
            ),
            new(
                nome: "Grand Theft Auto VI",
                preco: 599.9m,
                lancamento: Data("26/05/2026")
            ),
            new(
                nome: "God of War (2005)",
                preco: 199.9m,
                lancamento: Data("22/03/2005")
            ),
            new(
                nome: "God of War II",
                preco: 229.9m,
                lancamento: Data("13/03/2007")
            ),
            new(
                nome: "God of War III",
                preco: 279.9m,
                lancamento: Data("16/03/2010")
            ),
            new(
                nome: "God of War (2018)",
                preco: 299.9m,
                lancamento: Data("20/04/2018")
            ),
            new(
                nome: "God of War: Ragnarök",
                preco: 349.9m,
                lancamento: Data("09/11/2022")
            ),
            new(
                nome: "Battlefield 3",
                preco: 199.9m,
                lancamento: Data("25/10/2011")
            ),
            new(
                nome: "Battlefield 4",
                preco: 229.9m,
                lancamento: Data("29/10/2013")
            ),
            new(
                nome: "Battlefield 1",
                preco: 249.9m,
                lancamento: Data("21/10/2016")
            ),
            new(
                nome: "Battlefield V",
                preco: 279.9m,
                lancamento: Data("20/11/2018")
            ),
            new(
                nome: "Battlefield 2042",
                preco: 319.9m,
                lancamento: Data("19/11/2021")
            ),
            new(
                nome: "Battlefield 6",
                preco: 529.9m,
                lancamento: Data("10/10/2025")
            ),
            new(
                nome: "Marvel's Spider-Man",
                preco: 299.9m,
                lancamento: Data("07/09/2018")
            ),
            new(
                nome: "Marvel's Spider-Man: Miles Morales",
                preco: 349.9m,
                lancamento: Data("12/11/2020")
            ),
            new(
                nome: "Marvel's Spider-Man 2",
                preco: 399.9m,
                lancamento: Data("20/10/2023")
            ),
            new(
                nome: "Red Dead Redemption",
                preco: 249.9m,
                lancamento: Data("18/05/2010")
            ),
            new(
                nome: "Red Dead Redemption 2",
                preco: 349.9m,
                lancamento: Data("26/10/2018")
            ),
            new(
                nome: "The Last of Us",
                preco: 299.9m,
                lancamento: Data("14/06/2013")
            ),
            new(
                nome: "The Last of Us Part II",
                preco: 349.9m,
                lancamento: Data("19/06/2020")
            ),
            new(
                nome: "Gran Turismo 4",
                preco: 199.9m,
                lancamento: Data("28/02/2005")
            ),
            new(
                nome: "Gran Turismo 7",
                preco: 349.9m,
                lancamento: Data("04/03/2022")
            ),
            new(
                nome: "Ride 5",
                preco: 299.9m,
                lancamento: Data("24/08/2023")
            ),
            new(
                nome: "FIFA 13",
                preco: 179.9m,
                lancamento: Data("25/09/2012")
            ),
            new(
                nome: "Forza Horizon",
                preco: 229.9m,
                lancamento: Data("23/10/2012")
            ),
            new(
                nome: "Forza Horizon 2",
                preco: 249.9m,
                lancamento: Data("30/09/2014")
            ),
            new(
                nome: "Forza Horizon 3",
                preco: 279.9m,
                lancamento: Data("27/09/2016")
            ),
            new(
                nome: "Forza Horizon 4",
                preco: 299.9m,
                lancamento: Data("02/10/2018")
            ),
            new(
                nome: "Forza Horizon 5",
                preco: 349.9m,
                lancamento: Data("09/11/2021")
            ),
            new(
                nome: "Forza Motorsport 4",
                preco: 199.9m,
                lancamento: Data("11/10/2011")
            ),
            new(
                nome: "Shadow of the Colossus",
                preco: 249.9m,
                lancamento: Data("18/10/2005")
            )
        });
    }

    private static DateTime Data(string data) =>
        DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);

    public async Task<IEnumerable<Jogo>> ObterJogosAsync(CancellationToken cancellationToken)
        => await Task.FromResult(_jogos);

    public async Task<IEnumerable<Jogo>> ObterJogosPorAnoLancamentoAsync(int anoLancamento, CancellationToken cancellationToken)
        => await Task.FromResult(_jogos.Where(x => x.Lancamento is DateTime lanc && lanc.Year == anoLancamento));

    public async Task<Jogo?> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken)
        => await Task.FromResult(_jogos.FirstOrDefault(jogo => jogo.Id == id));

    public async Task<Jogo?> ObterJogoPorNomeParcialAsync(string nome, CancellationToken cancellationToken)
        => await Task.FromResult(_jogos.FirstOrDefault(jogo => jogo.Nome!.Contains(nome, StringComparison.OrdinalIgnoreCase)));

    public async Task<Jogo> CriarJogoAsync(Jogo jogo, CancellationToken cancellationToken)
    {
        Jogo jogoCriado = new(
            nome: jogo.Nome,
            preco: jogo.Preco,
            lancamento: jogo.Lancamento
        );

        _jogos.Add(jogoCriado);

        return await Task.FromResult(jogoCriado);
    }

    public async Task<Jogo?> EditarJogoAsync(Guid id, Jogo jogo, CancellationToken cancellationToken)
    {
        Jogo? jogoParaAtualizar = await ObterJogoPorIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException("Jogo não encontrado");

        jogoParaAtualizar.Atualizar(jogo);

        return await Task.FromResult(jogoParaAtualizar);
    }

    public async Task DeletarJogoAsync(Guid id, CancellationToken cancellationToken)
    {
        Jogo? jogoParaDeletar = await ObterJogoPorIdAsync(id, cancellationToken) ??
            throw new KeyNotFoundException("Jogo não encontrado");

        _jogos.Remove(jogoParaDeletar);
    }
}
