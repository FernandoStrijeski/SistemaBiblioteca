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

            _logger.LogInformation("Obtenção de dados bem-sucedida.");

            return autores;
        }

        [HttpGet("PorNome/{nome}", Name = "GetAutoresPorNome")]
        public async Task<IEnumerable<Autor>> GetAutoresPorNome(string nome)
        {
            var autores = await _autoresService.GetAutoresPorNome(nome);

            return autores;
        }
        
        [HttpGet("PorParteDoNome/{nome}", Name = "GetAutoresPorNomeLike")]
        public async Task<IEnumerable<Autor>> GetAutoresPorNomeLike(string nome)
        {
            var autores = await _autoresService.GetAutoresPorNomeLike(nome);

            return autores;
        }

        [HttpGet("{id}", Name = "GetAutor")]

        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _autoresService.GetAutorPorId(id);

            if (autor == null)
            {
                return NotFound();
            }
            else
            {
                return autor;
            }
        }
        #endregion

        #region Inclusão de Autores
        [HttpPost]
        public ActionResult<Autor> PostAutor(Autor autor)
        {
            _autoresService.AddAutor(autor);

            return CreatedAtAction("GetAutor", new { id = autor.idAutor }, autor);
        }
        #endregion

        #region Edição de Autores
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(int id, Autor autor)
        {
            if (autor == null)
            {
                throw new ArgumentNullException("O autor não pode ser nulo");
            }

            if (id != autor.idAutor)
            {
                return BadRequest("O ID do autor não corresponde ao ID na URL");
            }

            try
            {
                _autoresService.EditAutor(autor);
            }
            catch (Exception)
            {
                var existeAutor = await _autoresService.GetAutorPorId(id);

                if (existeAutor == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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
                return NotFound();
            }

            _autoresService.DeleteAutor(autor);

            return NoContent();
        }
        #endregion

    }
}
