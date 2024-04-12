using API.Controllers;
using Dados.Livros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ORM;
using ORM.Response;
using Servicos.Livros;


namespace API.Tests
{
    [TestFixture]
    public class LivrosControllerTests
    {
        private LivrosController? _controller;
        private ILogger<LivrosController>? _logger;
        private ILogger<LivrosDados>? _loggerDados;
        private LivrosService? _livrosService;
        private LivrosDados? _livrosDados;

        [SetUp]
        public void Setup()
        {
            #region Setup DbContext
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<AppDbContext>();
            #endregion

            _loggerDados = new Mock<ILogger<LivrosDados>>().Object;
            _livrosDados = new LivrosDados(context, _loggerDados);
            _livrosService = new LivrosService(_livrosDados);
            _logger = new Mock<ILogger<LivrosController>>().Object;
            _controller = new LivrosController(_logger, context, _livrosService);
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
            _logger = null;
            _loggerDados = null;
            _livrosService = null;
            _livrosDados = null;
        }

        [Test]
        [Description("Verifica se a lista de livros é retornada corretamente.")]
        public async Task GetLivros_ReturnsListOfBooks()
        {
            if (_controller != null)
            {
                // Act
                var result = await _controller.GetLivros();

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<List<Livro>>(result);
            }
        }

        [Test]
        [Description("Verifica se a lista de livros por titulo é retornada corretamente.")]
        [TestCase("Teste", TestName = "GetLivrosPorTitulo_ReturnsListOfBooksWithValidName")]
        [TestCase("", TestName = "GetLivrosPorTitulo_ReturnsListOfBooksWithEmptyName")]
        [TestCase(null, TestName = "GetLivrosPorTitulo_ReturnsListOfBooksWithNullName")]
        public async Task GetLivrosPorTitulo_ReturnsListOfBooks(string titulo)
        {
            if (_controller != null)
            {
                // Act
                var result = await _controller.GetLivrosPorTitulo(titulo);

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<List<Livro>>(result);
            }
        }

        [Test]
        [Description("Verifica se um BadRequest é retornado ao tentar editar um livro diferente do id da URL.")]
        public async Task PutLivro_ReturnsBadRequestForDiffId()
        {
            if (_controller != null)
            {
                // Arrange
                Livro? livroNulo = new Livro() { idLivro = 2 };

                // Act
                var result = await _controller.PutLivro(1, livroNulo);

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }

        [Test]
        [Description("Verifica se uma Exception é retornado ao tentar editar um livro nulo.")]
        public async Task PutLivro_ThrowsExceptionForNullUser()
        {
            if (_controller != null)
            {
                // Arrange
                Livro? livroNulo = null;

                // Act
                Exception? exception = null;

                try
                {
                    await _controller.PutLivro(1, livroNulo);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                // Assert
                ClassicAssert.IsNotNull(exception);
                ClassicAssert.IsInstanceOf<ArgumentNullException>(exception);
            }
        }


        [Test]
        [Description("Verifica se a lista de livros por autor é retornada corretamente.")]
        [TestCase(1, TestName = "GetLivrosPorAutorId_ReturnsListOfBooksWithValidId")]
        [TestCase(0, TestName = "GetLivrosPorAutorId_ReturnsListOfBooksWithZeroValue")]
        [TestCase(null, TestName = "GGetLivrosPorAutorId_ReturnsListOfBooksWithNullValue")]
        public async Task GetLivrosPorAutorId_ReturnsListOfBooks(int id)
        {
            if (_controller != null)
            {
                // Act
                var result = await _controller.GetLivrosPorAutorId(id);

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<List<Livro>>(result);
            }
        }

    }
}