namespace FiapCloudGames.Domain.Entities;

public class BibliotecaJogo
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid JogoId { get; set; }
    public DateTime? CompradoEm { get; set; }

    public virtual Usuario? Usuario { get; set; }
    public virtual Jogo? Jogo { get; set; }
}