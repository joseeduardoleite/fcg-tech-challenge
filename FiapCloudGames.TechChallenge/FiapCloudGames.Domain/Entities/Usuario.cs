using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public ERole? Role { get; set; }
    public DateTime? CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }

    public Usuario() { }

    public Usuario(string nome, string email, string senha, ERole role, DateTime? criadoEm, DateTime? atualizadoEm)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Role = role;
        CriadoEm = criadoEm;
        AtualizadoEm = atualizadoEm;
    }

}