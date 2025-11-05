using BTG.Fondos.BLL.Interfaces;
using BTG.Fondos.DTO.Fondos.Request;
using BTG.Fondos.DTO.Fondos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTG.Fondos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FondosController : ControllerBase
    {
        private readonly IFondoService _fondoService;

        public FondosController(IFondoService fondoService)
        {
            _fondoService = fondoService;
        }

        [HttpPost("suscribir")]
        public async Task<IActionResult> Suscribir([FromBody] SuscripcionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                SuscripcionResponse resultado = await _fondoService.SuscribirAsync(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost("cancelar")]
        public async Task<IActionResult> Cancelar([FromBody] CancelacionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _fondoService.CancelarAsync(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("historial/{clienteId}")]
        public async Task<IActionResult> ObtenerHistorial(string clienteId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var historial = await _fondoService.ObtenerHistorialTransaccionesAsync(clienteId);

                if (historial == null || !historial.Any())
                    return NotFound(new { mensaje = "No se encontraron transacciones para el cliente." });

                return Ok(historial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado.", detalle = ex.Message });
            }
        }
    }
}
