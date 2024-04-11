using Dados.Livros;
using Microsoft.EntityFrameworkCore;
using ORM.Response;

namespace Servicos.Livros
{
    public class LivrosService : ILivrosService
    {
        #region Parametros e Construtor
        private ILivrosDados _livroDAO;

        public LivrosService(ILivrosDados livroDAO)
        {
            _livroDAO = livroDAO;
        }
        #endregion

        #region Listagem de Livros
        public async Task<IList<Livro>> GetLivros()
        {
            return await _livroDAO.GetLivros().ToListAsync();
        }

        public async Task<IEnumerable<Livro>> GetLivrosPorTitulo(string titulo)
        {
            return await _livroDAO.GetLivros().Where(l => l.titulo == titulo).ToListAsync();
        }

        public async Task<IEnumerable<Livro>> GetLivrosPorTituloLikeORderByAnoPublicacao(string titulo)
        {
            return await _livroDAO.GetLivros()
                    .Where(l => l.titulo != null && l.titulo.Contains(titulo))
                    .OrderBy(l => l.anoPublicacao)
                    .ToListAsync();
        }

        public async Task<IList<string?>> GetTitulosLivrosPorAnoPublicacao(int anoPublicacao)
        {
            return await _livroDAO.GetLivros()
            .Where(l => l.anoPublicacao == anoPublicacao)
            .Select(l => l.titulo)
            .ToListAsync();
        }

        public async Task<Livro?> GetLivroPorId(int id)
        {
            return await _livroDAO.GetLivros().FindAsync(id);
        }
        #endregion

        #region Inclusão de Livros

        public void AddLivro(Livro livro)
        {
            _livroDAO.AddLivro(livro);
        }

        public void AddLivros(IEnumerable<Livro> livros)
        {
            _livroDAO.AddLivros(livros);
        }

        #endregion

        #region Edição de Livros
        public void EditLivro(Livro livro)
        {
            _livroDAO.EditLivro(livro);
        }
        #endregion

        #region Exclusão de Livros
        public void DeleteLivro(Livro livro)
        {
            _livroDAO.DeleteLivro(livro);
        }
        #endregion

    }
}
