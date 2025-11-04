using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class BibliotecaJogoMap : IEntityTypeConfiguration<BibliotecaJogo>
{
    public void Configure(EntityTypeBuilder<BibliotecaJogo> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .HasMany(b => b.Jogos)
               .WithMany()
               .UsingEntity<Dictionary<string, object>>(
                    "BibliotecaJogo_Jogo",
                    j => j.HasOne<Jogo>().WithMany().HasForeignKey("JogoId").OnDelete(DeleteBehavior.Cascade),
                    b => b.HasOne<BibliotecaJogo>().WithMany().HasForeignKey("BibliotecaId").OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.HasKey("BibliotecaId", "JogoId");
                    });

        builder
            .Property(b => b.UsuarioId)
            .IsRequired();
    }
}
