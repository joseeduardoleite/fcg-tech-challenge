namespace FiapCloudGames.Application.Dtos;

public record BibliotecaJogoDto(
    Guid Id,
    Guid UsuarioId,
    string? UsuarioNome,
    IEnumerable<JogoDto>? Jogos
);