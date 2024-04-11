using ORM.Response;

namespace Servicos.Autores
{
    public interface IAutoresService
    {
        public Task<IList<Autor>> GetAutores();
        public Task<IEnumerable<Autor>> GetAutoresPorNome(string nome);
        public Task<IEnumerable<Autor>> GetAutoresPorNomeLike(string nome);        
        public Task<Autor?> GetAutorPorId(int id);
        public void AddAutor(Autor autor);
        public void AddAutores(IEnumerable<Autor> autores);
        public void EditAutor(Autor autor);
        public void DeleteAutor(Autor autor);
    }
}
