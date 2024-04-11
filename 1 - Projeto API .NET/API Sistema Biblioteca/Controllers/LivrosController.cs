using Microsoft.AspNetCore.Mvc;
using ORM;
using ORM.Response;
using Servicos.Livros;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        #region Parametros e Construtor
        private readonly ILogger<LivrosController> _logger;
        private readonly AppDbContext _contexto;
        private ILivrosService _livrosService;

        public LivrosController(ILogger<LivrosController> logger, AppDbContext contexto, ILivrosService livrosService)
        {
            _logger = logger;
            _contexto = contexto;
            _livrosService = livrosService;
        }
        #endregion

        #region Listagem de Livros
        [HttpGet(Name = "GetLivros")]
        public async Task<IEnumerable<Livro>> GetLivros()
        {
            var livros = await _livrosService.GetLivros();

            _logger.LogInformation("Obtenção de dados bem-sucedida.");

            return livros;
        }

        [HttpGet("PorTitulo/{titulo}", Name = "GetLivrosPorTitulo")]
        public async Task<IEnumerable<Livro>> GetLivrosPorTitulo(string titulo)
        {
            var livros = await _livrosService.GetLivrosPorTitulo(titulo);

            return livros;
        }
        
        [HttpGet("PorParteDoTitulo/{titulo}", Name = "GetLivrosPorTituloLikeORderByAnoPublicacao")]
        public async Task<IEnumerable<Livro>> GetLivrosPorTituloLikeORderByAnoPublicacao(string titulo)
        {
            var livros = await _livrosService.GetLivrosPorTituloLikeORderByAnoPublicacao(titulo);

            return livros;
        }

        [HttpGet("{id}", Name = "GetLivro")]

        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _livrosService.GetLivroPorId(id);

            if (livro == null)
            {
                return NotFound();
            }
            else
            {
                return livro;
            }
        }
        #endregion

        #region Inclusão de Livro
        [HttpPost]
        public ActionResult<Livro> PostLivro(Livro livro)
        {            
            _livrosService.AddLivro(livro);

            return CreatedAtAction("GetLivro", new { id = livro.idLivro }, livro);

        }
        #endregion

        #region Edição de Livro
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            if (livro == null)
            {
                throw new ArgumentNullException("O livro não pode ser nulo");
            }

            if (id != livro.idLivro)
            {
                return BadRequest("O ID do livro não corresponde ao ID na URL");
            }

            try
            {
                _livrosService.EditLivro(livro);
            }
            catch (Exception)
            {
                var existeLivro = await _livrosService.GetLivroPorId(id);

                if (existeLivro == null)
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

        #region Exclusão de Livros

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _livrosService.GetLivroPorId(id);

            if (livro == null)
            {
                return NotFound();
            }

            _livrosService.DeleteLivro(livro);

            return NoContent();
        }
        #endregion

    }
}
