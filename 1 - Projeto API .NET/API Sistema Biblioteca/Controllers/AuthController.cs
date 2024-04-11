using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using ORM.Response;
//using ORM.Request;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Seu método GerarToken
        private string GerarToken(Usuario usuario)
        {
            if (usuario is { } && !string.IsNullOrEmpty(usuario.nome) && !string.IsNullOrEmpty(usuario.email))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, value: usuario.nome),
                    new Claim(ClaimTypes.Email, value: usuario.email)
                };

                string jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("A chave JWT não está configurada corretamente.");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30), // Tempo de expiração do token
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            else
            {
                throw new ArgumentNullException(nameof(usuario), "Usuário não pode ser nulo");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario login)
        {
            // Lógica de autenticação
            var token = GerarToken(login);

            return Ok(new { token });
        }
    }
}
