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

            _logger.LogInformation("Obten��o de dados bem-sucedida.");
            return livros;
        }

        [HttpGet("PorTitulo/{titulo}", Name = "GetLivrosPorTitulo")]
        public async Task<IEnumerable<Livro>> GetLivrosPorTitulo(string titulo)
        {
            var livros = await _livrosService.GetLivrosPorTitulo(titulo);
            
            _logger.LogInformation("Opera��o bem-sucedida.");
            return livros;
        }
        
        [HttpGet("PorParteDoTitulo/{titulo}", Name = "GetLivrosPorTituloLikeORderByAnoPublicacao")]
        public async Task<IEnumerable<Livro>> GetLivrosPorTituloLikeORderByAnoPublicacao(string titulo)
        {
            var livros = await _livrosService.GetLivrosPorTituloLikeORderByAnoPublicacao(titulo);

            _logger.LogInformation("Obten��o de dados bem-sucedida.");
            return livros;
        }

        [HttpGet("{id}", Name = "GetLivro")]

        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _livrosService.GetLivroPorId(id);

            if (livro == null)
            {
                _logger.LogInformation("GetLivro - Livro n�o encontrado para o id informado.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation("GetLivro - Obten��o de dados bem-sucedida.");
                return livro;
            }
        }
        #endregion


        #region Listagem de Livros por autor
        [HttpGet("LivrosPorAutorId/{id}", Name = "GetLivrosPorAutorId")]
        public async Task<IEnumerable<Livro>> GetLivrosPorAutorId(int id)
        {
            var livros = await _livrosService.GetLivrosPorAutorId(id);

            _logger.LogInformation("Opera��o bem-sucedida.");
            return livros;

        }
        #endregion

        #region Inclus�o de Livro
        [HttpPost]
        public ActionResult<Livro> PostLivro(Livro livro)
        {            
            _livrosService.AddLivro(livro);
            _logger.LogInformation("PostLivro - Opera��o bem-sucedida.");
            return CreatedAtAction("GetLivro", new { id = livro.idLivro }, livro);
        }
        #endregion

        #region Edi��o de Livro
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro? livro)
        {
            if (livro == null)
            {
                _logger.LogError("PutLivro - O livro n�o pode ser nulo.");
                throw new ArgumentNullException("O livro n�o pode ser nulo");
            }

            if (id != livro.idLivro)
            {
                _logger.LogInformation("PutLivro - O ID do livro n�o corresponde ao ID na URL.");
                return BadRequest("O ID do livro n�o corresponde ao ID na URL");
            }

            try
            {
                _livrosService.EditLivro(livro);
                _logger.LogInformation("PutLivro - Opera��o bem-sucedida.");

            }
            catch (Exception ex)
            {
                var existeLivro = await _livrosService.GetLivroPorId(id);

                if (existeLivro == null)
                {
                    _logger.LogInformation("PutLivro - Livro n�o encontrado para o id informado.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Ocorreu um erro: {ex.Message} - {ex.StackTrace}");
                    throw;
                }
            }

            _logger.LogInformation("PutLivro - Opera��o bem-sucedida.");
            return NoContent();
        }
        #endregion

        #region Exclus�o de Livros

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _livrosService.GetLivroPorId(id);

            if (livro == null)
            {
                _logger.LogInformation("DeleteLivro - Livro n�o encontrado para o id informado.");
                return NotFound();
            }

            _livrosService.DeleteLivro(livro);
            _logger.LogInformation("Opera��o bem-sucedida.");
            return NoContent();
        }
        #endregion

    }
}
