namespace FiapCloudGames.Domain.Entities;

public sealed class Jogo
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public decimal? Preco { get; set; }
    public DateTime? Lancamento { get; set; }
    public DateTime? CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }

    public Jogo() { }

    public Jogo(string? nome, decimal? preco, DateTime? lancamento)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Preco = preco;
        Lancamento = lancamento;
        CriadoEm = DateTime.UtcNow;
    }

    public void Atualizar(Jogo jogo)
    {
        Nome = jogo.Nome is not null ? jogo.Nome : Nome;
        Preco = jogo.Preco is not null ? jogo.Preco : Preco;
        Lancamento = jogo.Lancamento is not null ? jogo.Lancamento : Lancamento;
        AtualizadoEm = DateTime.UtcNow;
    }
}