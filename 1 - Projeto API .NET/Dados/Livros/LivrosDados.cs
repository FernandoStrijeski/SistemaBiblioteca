using Dados.Autores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ORM;
using ORM.Response;

namespace Dados.Livros
{
    public class LivrosDados : ILivrosDados
    {
        #region Parametros e Construtor
        private AppDbContext _contexto;
        private readonly ILogger<LivrosDados> _logger;

        public LivrosDados(AppDbContext contexto, ILogger<LivrosDados> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }
        #endregion

        #region Listagem de Livros
        public DbSet<Livro> GetLivros()
        {
            return _contexto.Livros;
        }
        #endregion

        #region Inclusão de Livros
        public void AddLivro(Livro livroInputModel)
        {
            try
            {
                _contexto.Livros.Add(livroInputModel);
                _logger.LogTrace("Vai adicionar um livro no banco.");
                _contexto.SaveChanges();
                _logger.LogTrace("Adicionado o livro com sucesso no banco.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Erro ao adicionar o livro: {ex.Message}");
                throw new Exception("Erro ao adicionar livro", ex);
            }           
        }

        public void AddLivros(IEnumerable<Livro> livrosInputModel)
        {
            _contexto.Livros.AddRange(livrosInputModel);
            _contexto.SaveChangesAsync();
        }
        #endregion

        #region Edição de Livros
        public void EditLivro(Livro livro)
        {
            var livroParaAtualizar = this.GetLivros().Find(livro.idLivro);

            if (livroParaAtualizar != null)
            {
                _contexto.Entry(livroParaAtualizar).CurrentValues.SetValues(livro);
                _contexto.SaveChanges();
            }
            else
            {
                _logger.LogError("Livro não encontrado para o id informado.");
                throw new Exception();
            }
        }
        #endregion

        #region Exclusão de Livros
        public void DeleteLivro(Livro livro)
        {
            var livrooParaDeletar = this.GetLivros().Find(livro.idLivro);

            if (livrooParaDeletar != null)
            {
                _contexto.Remove(livrooParaDeletar);
                _contexto.SaveChanges();
            }
            else
            {
                _logger.LogError("Livro não encontrado para o id informado.");
                throw new Exception();
            }
        }
        #endregion
    }
}
