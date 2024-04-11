using Dados.Autores;
using Microsoft.EntityFrameworkCore;
using ORM.Response;

namespace Servicos.Autores
{
    public class AutoresService : IAutoresService
    {
        #region Parametros e Construtor
        private IAutoresDados _autorDAO;

        public AutoresService(IAutoresDados autorDAO)
        {
            _autorDAO = autorDAO;
        }
        #endregion

        #region Listagem de Autores
        public async Task<IList<Autor>> GetAutores()
        {
            return await _autorDAO.GetAutores().ToListAsync();
        }

        public async Task<IEnumerable<Autor>> GetAutoresPorNome(string nome)
        {
            return await _autorDAO.GetAutores().Where(l => l.nome == nome).ToListAsync();
        }

        public async Task<IEnumerable<Autor>> GetAutoresPorNomeLike(string nome)
        {
            return await _autorDAO.GetAutores()
                    .Where(l => l.nome != null && l.nome.Contains(nome))
                    .OrderBy(l => l.nome)
                    .ToListAsync();
        }

        public async Task<Autor?> GetAutorPorId(int id)
        {
            return await _autorDAO.GetAutores().FindAsync(id);
        }
        #endregion

        #region Inclusão de Autores

        public void AddAutor(Autor autor)
        {
            _autorDAO.AddAutor(autor);
        }

        public void AddAutores(IEnumerable<Autor> autores)
        {
            _autorDAO.AddAutores(autores);
        }

        #endregion

        #region Edição de Autores
        public void EditAutor(Autor autor)
        {
            _autorDAO.EditAutor(autor);
        }
        #endregion

        #region Exclusão de Autores
        public void DeleteAutor(Autor autor)
        {
            _autorDAO.DeleteAutor(autor);
        }
        #endregion

    }
}
