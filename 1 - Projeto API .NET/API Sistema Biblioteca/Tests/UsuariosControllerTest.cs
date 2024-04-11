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


namespace API.Tests
{
    [TestFixture]
    public class UsuariosControllerTests
    {
        private UsuariosController? _controller;
        private ILogger<UsuariosController>? _logger;
        private UsuariosService? _usuariosService;
        private UsuariosDados? _usuariosDados;

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

            _usuariosDados = new UsuariosDados(context);
            _usuariosService = new UsuariosService(_usuariosDados);
            _logger = new Mock<ILogger<UsuariosController>>().Object;
            _controller = new UsuariosController(_logger, context, _usuariosService);
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
            _logger = null;
            _usuariosService = null;
            _usuariosDados = null;
        }

        [Test]
        [Description("Verifica se a lista de usuários é retornada corretamente.")]
        public async Task GetUsuarios_ReturnsListOfUsers()
        {
            if (_controller != null)
            {
                // Act
                var result = await _controller.GetUsuarios();

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<List<Usuario>>(result);
            }
        }

        [Test]
        [Description("Verifica se a lista de usuários por nome é retornada corretamente.")]
        [TestCase("Teste", TestName = "GetUsuariosPorNome_ReturnsListOfUsersWithValidName")]
        [TestCase("", TestName = "GetUsuariosPorNome_ReturnsListOfUsersWithEmptyName")]
        [TestCase(null, TestName = "GetUsuariosPorNome_ReturnsListOfUsersWithNullName")]
        public async Task GetUsuariosPorNome_ReturnsListOfUsers(string nome)
        {
            if (_controller != null)
            {
                // Act
                var result = await _controller.GetUsuariosPorNome(nome);

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<List<Usuario>>(result);
            }
        }

        [Test]
        [Description("Verifica se um BadRequest é retornado ao tentar editar um usuário diferente do id da URL.")]
        public async Task EditUsuario_ReturnsBadRequestForDiffId()
        {
            if (_controller != null)
            {
                // Arrange
                Usuario? usuarioNulo = new Usuario() { userId = 2 };

                // Act
                var result = await _controller.PutUsuario(1, usuarioNulo);

                // Assert
                ClassicAssert.IsNotNull(result);
                ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }

        [Test]
        [Description("Verifica se uma Exception é retornado ao tentar editar um usuário nulo.")]
        public async Task EditUsuario_ThrowsExceptionForNullUser()
        {
            if (_controller != null)
            {
                // Arrange
                Usuario? usuarioNulo = null;

                // Act
                Exception? exception = null;
                try
                {
                    await _controller.PutUsuario(1, usuarioNulo);
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