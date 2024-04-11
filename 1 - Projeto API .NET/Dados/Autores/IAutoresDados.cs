using Microsoft.EntityFrameworkCore;
using ORM.Response;

namespace Dados.Autores
{
    public interface IAutoresDados
    {
        public DbSet<Autor> GetAutores();

        public void AddAutor(Autor autor);

        public void AddAutores(IEnumerable<Autor> autores);

        public void EditAutor(Autor autor);

        public void DeleteAutor(Autor autor);

    }
}
