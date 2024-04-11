using Microsoft.EntityFrameworkCore;
using ORM.Response;

namespace ORM
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(u => u.userId);

            modelBuilder.Entity<Livro>(l =>
            { 
                l.HasKey(la => new { la.idLivro, la.idAutor });
                l.Property(e => e.idLivro).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Livro>()
            .HasKey(l => l.idLivro);

            modelBuilder.Entity<Livro>()
                .Property(l => l.idLivro)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.autor)
                .WithMany(a => a.livros)
                .HasForeignKey(l => l.idAutor)
                .OnDelete(DeleteBehavior.Restrict); 
         
            modelBuilder.Entity<Autor>()
                .HasKey(a => a.idAutor);


        }
    }
}
