using BTG.Fondos.API.Controllers;
using BTG.Fondos.BLL.Interfaces;
using BTG.Fondos.DTO.Fondos.Request;
using BTG.Fondos.DTO.Fondos.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BTG.Fondos.Test
{
    public class FondosControllerTests
    {
        private readonly Mock<IFondoService> _mockService;
        private readonly FondosController _controller;

        public FondosControllerTests()
        {
            _mockService = new Mock<IFondoService>();
            _controller = new FondosController(_mockService.Object);
        }

        // ---------- TEST 1: SUSCRIPCIÓN EXITOSA ----------
        [Fact]
        public async Task Suscribir_Solicitud_Valida()
        {
            var request = new SuscripcionRequest
            {
                IdCliente = "1",
                IdFondo = "FPV",
                Monto = 75000
            };

            var response = new SuscripcionResponse
            {
                IdTransaccion = "TX123",
                Mensaje = "Suscripción exitosa al fondo FPV",
                SaldoActual = 100000
            };

            _mockService.Setup(s => s.SuscribirAsync(It.IsAny<SuscripcionRequest>()))
                        .ReturnsAsync(response);

            var result = await _controller.Suscribir(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<SuscripcionResponse>(okResult.Value);
            Assert.Equal("Suscripción exitosa al fondo FPV", data.Mensaje);
            Assert.Equal(200, okResult.StatusCode);
        }

        // ---------- TEST 2: CANCELACIÓN EXITOSA ----------
        [Fact]
        public async Task Cancelar_Exitosa()
        {
            var request = new CancelacionRequest
            {
                IdCliente = "1",
                IdFondo = "FPV"
            };

            var response = new CancelacionResponse
            {
                IdTransaccion = "TX999",
                Mensaje = "Suscripción al fondo FPV cancelada. Monto devuelto: $75,000.00",
                SaldoActual = 175000
            };

            _mockService.Setup(s => s.CancelarAsync(It.IsAny<CancelacionRequest>()))
                        .ReturnsAsync(response);

            var result = await _controller.Cancelar(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<CancelacionResponse>(okResult.Value);
            Assert.Contains("cancelada", data.Mensaje);
            Assert.Equal(175000, data.SaldoActual);
        }

        // ----------TEST 3: CONSULTA HISTORIAL EXITOSO ----------
        [Fact]
        public async Task Obtener_Historial_Transacciones()
        {
            var historial = new List<HistorialTransaccionResponse>
            {
                new HistorialTransaccionResponse
                {
                    IdTransaccion = "T1",
                    NombreFondo = "FPV",
                    Tipo = "Suscripción",
                    Monto = 75000,
                    Fecha = System.DateTime.UtcNow
                },
                new HistorialTransaccionResponse
                {
                    IdTransaccion = "T2",
                    NombreFondo = "FNA",
                    Tipo = "Cancelación",
                    Monto = 50000,
                    Fecha = System.DateTime.UtcNow.AddDays(-1)
                }
            };

            _mockService.Setup(s => s.ObtenerHistorialTransaccionesAsync("1"))
                        .ReturnsAsync(historial);

            var result = await _controller.ObtenerHistorial("1");
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<List<HistorialTransaccionResponse>>(okResult.Value);
            Assert.Equal(2, data.Count);
            Assert.Equal("FPV", data[0].NombreFondo);
        }

        // ---------- TEST 4: HISTORIAL VACÍO ----------
        [Fact]
        public async Task Obtener_Historial_Sin_Transacciones()
        {
            _mockService.Setup(s => s.ObtenerHistorialTransaccionesAsync("99"))
                        .ReturnsAsync(new List<HistorialTransaccionResponse>());

            var result = await _controller.ObtenerHistorial("99");
            var notFound = Assert.IsType<NotFoundObjectResult>(result);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(notFound.Value);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;

            Assert.True(data.ContainsKey("mensaje"));
            Assert.Equal("No se encontraron transacciones para el cliente.", data["mensaje"]);
        }
    }
}
