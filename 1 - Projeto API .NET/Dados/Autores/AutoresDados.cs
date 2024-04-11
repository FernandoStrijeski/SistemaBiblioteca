using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ORM;
using ORM.Response;

namespace Dados.Autores
{
    public class AutoresDados : IAutoresDados
    {
        #region Parametros e Construtor
        private AppDbContext _contexto;
        private readonly ILogger<AutoresDados> _logger;

        public AutoresDados(AppDbContext contexto, ILogger<AutoresDados> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }
        #endregion

        #region Listagem de Autores
        public DbSet<Autor> GetAutores()
        {
            return _contexto.Autores;
        }
        #endregion

        #region Inclusão de Autores
        public void AddAutor(Autor autorInputModel)
        {
            try
            {
                _contexto.Autores.Add(autorInputModel);
                _logger.LogTrace("Vai adicionar um autor no banco.");
                _contexto.SaveChanges();
                _logger.LogTrace("Adicionado o autor com sucesso no banco.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Erro ao adicionar o autor: {ex.Message}");                
                throw new Exception("Erro ao adicionar autor", ex);
            }            
        }

        public void AddAutores(IEnumerable<Autor> autoresInputModel)
        {
            _contexto.Autores.AddRange(autoresInputModel);
            _contexto.SaveChanges();
        }
        #endregion

        #region Edição de Autores
        public void EditAutor(Autor autor)
        {
            var autorParaAtualizar = this.GetAutores().Find(autor.idAutor);

            if (autorParaAtualizar != null)
            {
                _contexto.Entry(autorParaAtualizar).CurrentValues.SetValues(autor);
                _contexto.SaveChanges();
            }
            else
            {
                _logger.LogError("Autor não encontrado para o id informado.");
                throw new Exception();
            }
        }
        #endregion

        #region Exclusão de Autores
        public void DeleteAutor(Autor autor)
        {
            var autorParaDeletar = this.GetAutores().Find(autor.idAutor);

            if (autorParaDeletar != null)
            {
                _contexto.Remove(autorParaDeletar);
                _contexto.SaveChanges();
            }
            else
            {
                _logger.LogError("Autor não encontrado para o id informado.");
                throw new Exception();
            }
        }
        #endregion
    }
}
