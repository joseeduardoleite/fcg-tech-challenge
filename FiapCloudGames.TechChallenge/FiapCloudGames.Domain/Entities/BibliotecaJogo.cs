namespace FiapCloudGames.Domain.Entities;

public class BibliotecaJogo
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
    public virtual ICollection<Jogo> Jogos { get; set; } = [];

    public BibliotecaJogo() { }

    public BibliotecaJogo(Guid usuarioId, ICollection<Jogo> jogos)
    {
        UsuarioId = usuarioId;
        Jogos = jogos;
    }
}