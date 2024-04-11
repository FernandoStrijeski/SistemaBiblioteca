using ORM;
using Servicos.Usuarios;
using Dados.Usuarios;
using API.Controllers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ORM.Response;
using Dados.Autores;
using Servicos.Autores;


namespace API.Tests
{
    [TestFixture]
    public class AutoresControllerTests
    {
        private AutoresController? _controller;
        private ILogger<AutoresController>? _logger;
        private ILogger<AutoresDados>? _loggerDados;
        private AutoresService? _autoresService;
        private AutoresDados? _autoresDados;

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

            _loggerDados = new Mock<ILogger<AutoresDados>>().Object;
            _autoresDados = new AutoresDados(context, _loggerDados);
            _autoresService = new AutoresService(_autoresDados);
            _logger = new Mock<ILogger<AutoresController>>().Object;
            _controller = new AutoresController(_logger, context, _autoresService);
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
            _logger = null;
            _loggerDados = null;
            _autoresService = null;
            _autoresDados = null;
        }

        [Test]
        [Description("Verifica se a lista de autores é retornada corretamente.")]
        public async Task GetAutores_ReturnsListOfAuthors()
        {
            if (_controller != null)
            {
                // Act
                var result = await _controller.GetAutores();

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<List<Autor>>(result);
            }
        }

        [Test]
        [Description("Verifica se a lista de autores por nome é retornada corretamente.")]
        [TestCase("Teste", TestName = "GetAutoresPorNome_ReturnsListOfAuthorsWithValidName")]
        [TestCase("", TestName = "GetAutoresPorNome_ReturnsListOfAuthorsWithEmptyName")]
        [TestCase(null, TestName = "GetAutoresPorNome_ReturnsListOfAuthorsWithNullName")]
        public async Task GetAutoresPorNome_ReturnsListOfAuthors(string nome)
        {
            if (_controller != null)
            {
                // Act
                var result = await _controller.GetAutoresPorNome(nome);

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<List<Autor>>(result);
            }
        }

        [Test]
        [Description("Verifica se um BadRequest é retornado ao tentar editar um autor diferente do id da URL.")]
        public async Task PutAutor_ReturnsBadRequestForDiffId()
        {
            if (_controller != null)
            {
                // Arrange
                Autor? autorNulo = new Autor() { idAutor = 2 };

                // Act
                var result = await _controller.PutAutor(1, autorNulo);

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }

        [Test]
        [Description("Verifica se uma Exception é retornado ao tentar editar um autor nulo.")]
        public async Task PutAutor_ThrowsExceptionForNullUser()
        {
            if (_controller != null)
            {
                // Arrange
                Autor? autorNulo = null;

                // Act
                Exception? exception = null;
                try
                {
                    await _controller.PutAutor(1, autorNulo);
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

    }
}