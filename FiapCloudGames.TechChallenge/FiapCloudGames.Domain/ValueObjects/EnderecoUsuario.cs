namespace FiapCloudGames.Domain.ValueObjects;

public record EnderecoUsuario(
    long Numero,
    string Logradouro,
    string Bairro,
    string Cidade,
    string Cep,
    string Estado
);