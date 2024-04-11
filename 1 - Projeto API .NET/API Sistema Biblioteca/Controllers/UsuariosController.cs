using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servicos.Usuarios;
using ORM;
using Microsoft.AspNetCore.Authorization;
using ORM.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        #region Parametros e Construtor
        private readonly ILogger<UsuariosController> _logger;
        private readonly AppDbContext _contexto;
        private IUsuariosService _usuariosService;

        public UsuariosController(ILogger<UsuariosController> logger, AppDbContext contexto, IUsuariosService usuariosService)
        {
            _logger = logger;
            _contexto = contexto;
            _usuariosService = usuariosService;
        }
        #endregion

        #region Listagem de Usu�rios
        [HttpGet(Name = "GetUsuarios")]
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            //select * from usuarios
            var usuarios = await _usuariosService.GetUsuarios();

            _logger.LogInformation("Obten��o de dados bem-sucedida.");

            return usuarios;
        }

        [HttpGet("PorNome/{nome}", Name = "GetUsuariosPorNome")]
        public async Task<IEnumerable<Usuario>> GetUsuariosPorNome(string nome)
        {
            //select * from usuarios where nome = ?
            var usuarios = await _usuariosService.GetUsuariosPorNome(nome);

            return usuarios;
        }

        [HttpGet("{id}", Name = "GetUsuario")]

        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            //select * from usuarios where id=?
            var usuario = await _usuariosService.GetUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                return usuario;
            }
        }
        #endregion

        #region Inclus�o de Usu�rios
        [HttpPost]
        public ActionResult<Usuario> PostUsuario(Usuario usuario)
        {
            usuario.dataCadastro = DateTime.Now; // Adicionando a data de cadastro

            _usuariosService.AddUsuario(usuario);

            return CreatedAtAction("GetUsuario", new { id = usuario.userId }, usuario);
        }
        #endregion

        #region Edi��o de Usu�rios
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("O usu�rio n�o pode ser nulo");
            }

            if (id != usuario.userId)
            {
                return BadRequest("O ID do usu�rio n�o corresponde ao ID na URL");
            }

            try
            {
                _usuariosService.EditUsuario(usuario);
            }
            catch (Exception)
            {
                var existeUsuario = await _usuariosService.GetUsuarioPorId(id);

                if (existeUsuario == null)
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

        #region Exclus�o de Usu�rios

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuariosService.GetUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _usuariosService.DeleteUsuario(usuario);

            return NoContent();
        }
        #endregion

    }
}
