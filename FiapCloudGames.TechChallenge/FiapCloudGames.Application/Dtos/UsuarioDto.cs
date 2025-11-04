using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Application.Dtos;

public record UsuarioDto(
    Guid? Id,
    string? Nome,
    string? Email,
    string? Senha,
    ERole? Role = null
);