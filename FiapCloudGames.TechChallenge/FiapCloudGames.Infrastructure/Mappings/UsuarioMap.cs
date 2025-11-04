using FiapCloudGames.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapCloudGames.Infrastructure.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Nome)
            .HasMaxLength(100);

        builder
            .Property(u => u.Email)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(u => u.Senha)
            .IsRequired();

        builder
            .Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();

        builder
            .HasOne(u => u.Biblioteca)
            .WithOne(b => b.Usuario)
            .HasForeignKey<BibliotecaJogo>(b => b.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.CriadoEm);

        builder.Property(u => u.AtualizadoEm);
    }
}
