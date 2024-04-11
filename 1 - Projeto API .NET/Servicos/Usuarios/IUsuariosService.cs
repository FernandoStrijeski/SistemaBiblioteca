using ORM.Response;

namespace Servicos.Usuarios
{
    public interface IUsuariosService
    {
        public Task<IList<Usuario>> GetUsuarios();

        public Task<IEnumerable<Usuario>> GetUsuariosPorNome(string nome);

        public Task<IEnumerable<Usuario>> GetUsuariosPorNomeLikeORderByDataCadastro(string nome);

        public Task<IList<string?>> GetNomesUsuariosPorAnoCadastro(int anoCadastro);

        public Task<Usuario?> GetUsuarioPorId(int id);

        public void AddUsuario(Usuario usuario);

        public void AddUsuarios(IEnumerable<Usuario> usuarios);

        public void EditUsuario(Usuario usuario);

        public void DeleteUsuario(Usuario usuario);
    }
}
