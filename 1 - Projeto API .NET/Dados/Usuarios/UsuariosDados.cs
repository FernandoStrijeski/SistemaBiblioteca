using Microsoft.EntityFrameworkCore;
using ORM;
using ORM.Response;

namespace Dados.Usuarios
{
    public class UsuariosDados : IUsuariosDados
    {
        #region Parametros e Construtor
        private AppDbContext _contexto;

        public UsuariosDados(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        #endregion

        #region Listagem de Usuários
        public DbSet<Usuario> GetUsuarios()
        {
            return _contexto.Usuarios;
        }
        #endregion

        #region Inclusão de Usuários
        public void AddUsuario(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChangesAsync();
        }

        public void AddUsuarios(IEnumerable<Usuario> usuarios)
        {
            _contexto.Usuarios.AddRange(usuarios);
            _contexto.SaveChangesAsync();
        }
        #endregion

        #region Edição de Usuários
        public void EditUsuario(Usuario usuario)
        {
            var usuarioParaAtualizar = this.GetUsuarios().Find(usuario.userId);

            if (usuarioParaAtualizar != null)
            {
                _contexto.Entry(usuarioParaAtualizar).CurrentValues.SetValues(usuario);
                _contexto.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }
        #endregion

        #region Exclusão de Usuários
        public void DeleteUsuario(Usuario usuario)
        {
            var usuarioParaDeletar = this.GetUsuarios().Find(usuario.userId);

            if (usuarioParaDeletar != null)
            {
                _contexto.Remove(usuarioParaDeletar);
                _contexto.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }
        #endregion
    }
}
