namespace FiapCloudGames.Domain.Entities;

public class Jogo
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public decimal? Preco { get; set; }
    public DateTime? LancadoEm { get; set; }
}