using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Infrastructure.Data;

[ExcludeFromCodeCoverage]
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Jogo> Jogos => Set<Jogo>();
    public DbSet<BibliotecaJogo> BibliotecasDeJogos => Set<BibliotecaJogo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new JogoMap());
        modelBuilder.ApplyConfiguration(new BibliotecaJogoMap());

        base.OnModelCreating(modelBuilder);
    }
}