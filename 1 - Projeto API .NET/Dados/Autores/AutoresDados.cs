using Microsoft.EntityFrameworkCore;
using ORM;
using ORM.Response;

namespace Dados.Autores
{
    public class AutoresDados : IAutoresDados
    {
        #region Parametros e Construtor
        private AppDbContext _contexto;

        public AutoresDados(AppDbContext contexto)
        {
            _contexto = contexto;
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
            _contexto.Autores.Add(autorInputModel);
            _contexto.SaveChanges();
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
                throw new Exception();
            }
        }
        #endregion
    }
}
