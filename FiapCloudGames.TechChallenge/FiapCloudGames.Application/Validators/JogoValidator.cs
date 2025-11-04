using FiapCloudGames.Application.Dtos;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Application.Validators;

[ExcludeFromCodeCoverage]
public sealed class JogoValidator : AbstractValidator<JogoDto>
{
    public JogoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
                .WithMessage("O nome do jogo é obrigatório.")
            .MaximumLength(150)
                .WithMessage("O nome do jogo deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Preco)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O preço não pode ser negativo.");

        RuleFor(x => x.Lancamento)
            .NotEmpty().WithMessage("A data de lançamento é obrigatória.");
    }
}