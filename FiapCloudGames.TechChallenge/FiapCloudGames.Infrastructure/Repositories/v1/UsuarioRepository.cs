using FiapCloudGames.Domain.Entities;
using FiapCloudGames.Domain.Repositories.v1;

namespace FiapCloudGames.Infrastructure.Repositories.v1;

public sealed class UsuarioRepository : IUsuarioRepository
{

    private static readonly List<Usuario> _usuarios = [];

    public static void Seed()
    {
        if (_usuarios.Count != 0)
            return;

        _usuarios.AddRange(new List<Usuario>()
        {
            new(
                nome: "José",
                email: "jose.fiap@gmail.com",
                senha: "JoseFiap1234",
                role: Domain.Enums.ERole.Admin
            ),
            new(
                nome: "Eduardo",
                email: "eduardo.fiap@gmail.com",
                senha: "EduardoFiap1234"
            ),
            new(
                nome: "João",
                email: "joão.fiap@gmail.com",
                senha: "JoaoFiap1234"
            ),
            new(
                nome: "Maria",
                email: "maria.fiap@gmail.com",
                senha: "MariaFiap1234"
            )
        });
    }

    public async Task<IEnumerable<Usuario>> ObterUsuariosAsync(CancellationToken cancellationToken)
        => await Task.FromResult(_usuarios);

    public async Task<Usuario?> ObterUsuarioPorIdAsync(Guid id, CancellationToken cancellationToken)
        => await Task.FromResult(_usuarios.FirstOrDefault(usuario => usuario.Id == id));

    public async Task<Usuario> CriarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        Usuario usuarioCriado = new(
            nome: usuario.Nome,
            email: usuario.Email,
            senha: usuario.Senha
        );

        _usuarios.Add(usuarioCriado);

        return await Task.FromResult(usuarioCriado);
    }

    public async Task<Usuario> EditarUsuarioAsync(Guid id, Usuario usuario, CancellationToken cancellationToken)
    {
        Usuario? usuarioParaAtualizar = await ObterUsuarioPorIdAsync(id, cancellationToken);

        if (usuarioParaAtualizar is null)
            throw new KeyNotFoundException("Usuário não encontrado");
        else
            usuarioParaAtualizar.Atualizar(usuario);

        return usuarioParaAtualizar;
    }

    public async Task DeletarUsuarioAsync(Guid id, CancellationToken cancellationToken)
    {
        Usuario? usuarioParaDeletar = await ObterUsuarioPorIdAsync(id, cancellationToken);

        if (usuarioParaDeletar is null)
            throw new KeyNotFoundException("Usuário não encontrado");
        else
            _usuarios.Remove(usuarioParaDeletar);
    }
}