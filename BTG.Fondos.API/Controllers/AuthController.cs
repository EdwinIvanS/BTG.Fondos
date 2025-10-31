using BTG.Fondos.Auth.Token;
using BTG.Fondos.DAL.Interfaces;
using BTG.Fondos.DTO.Auth.Request;
using BTG.Fondos.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Fondos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenService _tokenService;

        public AuthController(IUsuarioRepository usuarioRepository, TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisteRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingUser = await _usuarioRepository.GetByUsernameAsync(request.Username);
                if (existingUser != null)
                    return BadRequest("El nombre de usuario ya existe.");

                var user = new Usuario
                {
                    NombreUsuario = request.Username,
                    ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Email = request.Email,
                    Rol = request.Rol ?? "Cliente"
                };

                await _usuarioRepository.CrearUsuarioAsync(user);
                return Ok("Usuario registrado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTO.Auth.Request.LoginRequest request)
        {
            try {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _usuarioRepository.GetByUsernameAsync(request.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.ContrasenaHash))
                    return Unauthorized("Usuario o contraseña inválidos");

                var token = _tokenService.GenerarToken(user);
                return Ok(new { token, rol = user.Rol });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message});
            }
        }
    }
}
