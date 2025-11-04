using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class JogoMap : IEntityTypeConfiguration<Jogo>
{
    public void Configure(EntityTypeBuilder<Jogo> builder)
    {
        builder.HasKey(j => j.Id);

        builder.Property(j => j.Nome)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(j => j.Preco);

        builder.Property(j => j.Lancamento);

        builder.Property(j => j.CriadoEm);

        builder.Property(j => j.AtualizadoEm);
    }
}
