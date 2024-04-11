using Microsoft.EntityFrameworkCore;
using ORM;
using ORM.Response;

namespace Dados.Livros
{
    public class LivrosDados : ILivrosDados
    {
        #region Parametros e Construtor
        private AppDbContext _contexto;

        public LivrosDados(AppDbContext contexto)
        {
            _contexto = contexto;         
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
            _contexto.Livros.Add(livroInputModel);
            _contexto.SaveChanges();
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
                throw new Exception();
            }
        }
        #endregion
    }
}
