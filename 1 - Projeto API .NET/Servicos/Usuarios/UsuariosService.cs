using Dados.Usuarios;
using Microsoft.EntityFrameworkCore;
using ORM.Response;


namespace Servicos.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        #region Parametros e Construtor
        private IUsuariosDados _usuarioDAO;

        public UsuariosService(IUsuariosDados usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
        }
        #endregion

        #region Listagem de Usuários
        public async Task<IList<Usuario>> GetUsuarios()
        {
            return await _usuarioDAO.GetUsuarios().ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosPorNome(string nome)
        {
            return await _usuarioDAO.GetUsuarios().Where(u => u.nome == nome).ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosPorNomeLikeORderByDataCadastro(string nome)
        {
            return await _usuarioDAO.GetUsuarios()
                    .Where(u => u.nome != null && u.nome.Contains(nome))
                    .OrderBy(u => u.dataCadastro)
                    .ToListAsync();
        }

        public async Task<IList<string?>> GetNomesUsuariosPorAnoCadastro(int anoCadastro)
        {
            return await _usuarioDAO.GetUsuarios()
            .Where(u => u.dataCadastro.Year == anoCadastro)
            .Select(u => u.nome)
            .ToListAsync();
        }

        public async Task<Usuario?> GetUsuarioPorId(int id)
        {
            return await _usuarioDAO.GetUsuarios().FindAsync(id);
        }
        #endregion

        #region Inclusão de Usuários

        public void AddUsuario(Usuario usuario)
        {
            _usuarioDAO.AddUsuario(usuario);
        }

        public void AddUsuarios(IEnumerable<Usuario> usuarios)
        {
            _usuarioDAO.AddUsuarios(usuarios);
        }

        #endregion

        #region Edição de Usuários
        public void EditUsuario(Usuario usuario)
        {
           _usuarioDAO.EditUsuario(usuario);
        }
        #endregion

        #region Exclusão de Usuários
        public void DeleteUsuario(Usuario usuario)
        {
            _usuarioDAO.DeleteUsuario(usuario);
        }
        #endregion

    }
}
