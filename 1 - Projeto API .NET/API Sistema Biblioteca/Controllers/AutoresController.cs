using Microsoft.AspNetCore.Mvc;
using ORM;
using ORM.Response;
using Servicos.Autores;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        #region Parametros e Construtor
        private readonly ILogger<AutoresController> _logger;
        private readonly AppDbContext _contexto;
        private IAutoresService _autoresService;

        public AutoresController(ILogger<AutoresController> logger, AppDbContext contexto, IAutoresService autoresService)
        {
            _logger = logger;
            _contexto = contexto;
            _autoresService = autoresService;
        }
        #endregion

        #region Listagem de Autores
        [HttpGet(Name = "GetAutores")]
        public async Task<IEnumerable<Autor>> GetAutores()
        {
            var autores = await _autoresService.GetAutores();

            _logger.LogInformation("GetAutores - Obtenção de dados bem-sucedida.");

            return autores;
        }

        [HttpGet("PorNome/{nome}", Name = "GetAutoresPorNome")]
        public async Task<IEnumerable<Autor>> GetAutoresPorNome(string nome)
        {
            var autores = await _autoresService.GetAutoresPorNome(nome);

            _logger.LogInformation("GetAutoresPorNome - Obtenção de dados bem-sucedida.");

            return autores;
        }
        
        [HttpGet("PorParteDoNome/{nome}", Name = "GetAutoresPorNomeLike")]
        public async Task<IEnumerable<Autor>> GetAutoresPorNomeLike(string nome)
        {
            var autores = await _autoresService.GetAutoresPorNomeLike(nome);

            _logger.LogInformation("GetAutoresPorNomeLike - Obtenção de dados bem-sucedida.");

            return autores;
        }

        [HttpGet("{id}", Name = "GetAutor")]

        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _autoresService.GetAutorPorId(id);

            if (autor == null)
            {
                _logger.LogInformation("GetAutor - Autor não encontrado para o id informado.");

                return NotFound();
            }
            else
            {
                _logger.LogInformation("GetAutor - Obtenção de dados bem-sucedida.");

                return autor;
            }
        }
        #endregion

        #region Inclusão de Autores
        [HttpPost]
        public ActionResult<Autor> PostAutor(Autor autor)
        {
            _autoresService.AddAutor(autor);

            _logger.LogInformation("PostAutor - Operação bem-sucedida.");

            return CreatedAtAction("GetAutor", new { id = autor.idAutor }, autor);
        }
        #endregion

        #region Edição de Autores
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor? autor)
        {
            if (autor == null)
            {
                _logger.LogInformation("PutAutor - O autor não pode ser nulo.");

                throw new ArgumentNullException("O autor não pode ser nulo");
            }

            if (id != autor.idAutor)
            {
                _logger.LogError("PutAutor - O ID do autor não corresponde ao ID na URL");
                return BadRequest("O ID do autor não corresponde ao ID na URL");
            }

            try
            {
                _autoresService.EditAutor(autor);
            }
            catch (Exception ex)
            {
                var existeAutor = await _autoresService.GetAutorPorId(id);

                if (existeAutor == null)
                {
                    _logger.LogInformation("PutAutor - Autor não encontrado para o id informado.");

                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Ocorreu um erro: {ex.Message} - {ex.StackTrace}");
                    throw;
                }
            }

            _logger.LogInformation("PutAutor - Operação bem-sucedida.");
            return NoContent();
        }
        #endregion

        #region Exclusão de Autores

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            var autor = await _autoresService.GetAutorPorId(id);

            if (autor == null)
            {
                _logger.LogInformation("Autor não encontrado para o id informado.");
                return NotFound();
            }

            _autoresService.DeleteAutor(autor);

            _logger.LogInformation("Operação 'DeleteAutor' bem-sucedida.");

            return NoContent();
        }
        #endregion

    }
}
