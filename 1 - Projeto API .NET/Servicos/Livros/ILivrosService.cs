using ORM.Response;

namespace Servicos.Livros
{
    public interface ILivrosService
    {
        public Task<IList<Livro>> GetLivros();

        public Task<IEnumerable<Livro>> GetLivrosPorTitulo(string nome);

        public Task<IEnumerable<Livro>> GetLivrosPorTituloLikeORderByAnoPublicacao(string nome);

        public Task<IList<string?>> GetTitulosLivrosPorAnoPublicacao(int anoCadastro);

        public Task<Livro?> GetLivroPorId(int id);

        public void AddLivro(Livro Livro);

        public void AddLivros(IEnumerable<Livro> Livros);

        public void EditLivro(Livro Livro);

        public void DeleteLivro(Livro Livro);

        public Task<IEnumerable<Livro>> GetLivrosPorAutorId(int id);
    }
}
