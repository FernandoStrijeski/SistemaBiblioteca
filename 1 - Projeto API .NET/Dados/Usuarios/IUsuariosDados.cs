using Microsoft.EntityFrameworkCore;
using ORM.Response;

namespace Dados.Usuarios
{
    public interface IUsuariosDados
    {
        public DbSet<Usuario> GetUsuarios();

        public void AddUsuario(Usuario usuario);

        public void AddUsuarios(IEnumerable<Usuario> usuarios);

        public void EditUsuario(Usuario usuario);

        public void DeleteUsuario(Usuario usuario);

    }
}
