using FiapCloudGames.Application.Dtos;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application.Validators;

[ExcludeFromCodeCoverage]
public sealed class BibliotecaJogoValidator : AbstractValidator<BibliotecaJogoDto>
{
    public BibliotecaJogoValidator()
    {
        RuleFor(x => x.UsuarioId)
            .NotEmpty()
            .WithMessage("O usuário é obrigatório.");

        RuleFor(x => x.Jogos)
            .NotNull()
                .WithMessage("A biblioteca deve conter uma lista de jogos.")
            .Must(jogos => jogos!.Any())
                .WithMessage("A biblioteca deve conter ao menos um jogo.");

        RuleForEach(x => x.Jogos)
            .SetValidator(new JogoValidator());
    }
}