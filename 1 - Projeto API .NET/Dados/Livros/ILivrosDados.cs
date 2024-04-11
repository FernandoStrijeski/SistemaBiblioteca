using Microsoft.EntityFrameworkCore;
using ORM.Response;

namespace Dados.Livros
{
    public interface ILivrosDados
    {
        public DbSet<Livro> GetLivros();

        public void AddLivro(Livro livro);

        public void AddLivros(IEnumerable<Livro> livros);

        public void EditLivro(Livro livro);

        public void DeleteLivro(Livro livro);

    }
}
