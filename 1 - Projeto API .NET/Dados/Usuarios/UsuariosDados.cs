using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ORM;
using ORM.Response;

namespace Dados.Usuarios
{
    public class UsuariosDados : IUsuariosDados
    {
        #region Parametros e Construtor
        private AppDbContext _contexto;
        private readonly ILogger<UsuariosDados> _logger;

        public UsuariosDados(AppDbContext contexto, ILogger<UsuariosDados> logger)
        {
            _contexto = contexto;
            _logger = logger;
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
         
            try
            {
                _contexto.Usuarios.Add(usuario);
                _logger.LogTrace("Vai adicionar um usuário no banco.");                
                _contexto.SaveChanges();
                _logger.LogTrace("Adicionado o usuário com sucesso no banco.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Erro ao adicionar o usuário: {ex.Message}");
                throw new Exception("Erro ao adicionar usuário", ex);
            }
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
                _logger.LogError("Usuário não encontrado para o id informado.");
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
                _logger.LogError("Usuário não encontrado para o id informado.");
                throw new Exception();
            }
        }
        #endregion
    }
}
