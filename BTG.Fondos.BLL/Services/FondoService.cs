using BTG.Fondos.BLL.Interfaces;
using BTG.Fondos.DAL.Interfaces;
using BTG.Fondos.DTO.Fondos.Request;
using BTG.Fondos.DTO.Fondos.Response;
using BTG.Fondos.DTO.Models;

namespace BTG.Fondos.BLL.Services
{
    public class FondoService : IFondoService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IFondoRepository _fondosRepository;
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly INotificationService _notificationService;

        public FondoService(
            IClienteRepository clienteRepository,
            IFondoRepository fondosRepository,
            ITransaccionRepository transaccionRepository,
            INotificationService notificationService
        )
        {
            _clienteRepository = clienteRepository;
            _fondosRepository = fondosRepository;
            _transaccionRepository = transaccionRepository;
            _notificationService = notificationService;
        }

        public async Task<SuscripcionResponse> SuscribirAsync(SuscripcionRequest request)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.IdCliente);
            var fondo = await _fondosRepository.GetByIdAsync(request.IdFondo);

            if (cliente == null || fondo == null)
                throw new InvalidOperationException("Cliente o fondo no encontrado.");

            if (request.Monto < fondo.MontoMinimo)
                throw new InvalidOperationException($"El monto mínimo para {fondo.Nombre} es {fondo.MontoMinimo:C}");

            if (cliente.Saldo < request.Monto)
                throw new InvalidOperationException($"No tiene saldo disponible para vincularse al fondo {fondo.Nombre}");

            cliente.Saldo -= request.Monto;
            await _clienteRepository.UpdateSaldoAsync(cliente.Id, cliente.Saldo);

            var transaccion = new Transaccion
            {
                Id = Guid.NewGuid().ToString(),
                IdCliente = cliente.Id,
                IdFondo = fondo.Id,
                Tipo = "Apertura",
                Monto = request.Monto,
                Fecha = DateTime.UtcNow
            };

            await _transaccionRepository.InsertAsync(transaccion);

            //Notificacion
            string mensaje = $"Suscripción exitosa al fondo {fondo.Nombre} por un monto de {request.Monto:C}.";

            if (cliente.PreferenciaNotificacion == "Email")
                await _notificationService.EnviarEmailAsync(cliente.PreferenciaNotificacion, "Suscripción exitosa", mensaje);

            else if (cliente.PreferenciaNotificacion == "SMS")
                await _notificationService.EnviarSmsAsync(cliente.PreferenciaNotificacion, mensaje);

            return new SuscripcionResponse
            {
                IdTransaccion = transaccion.Id,
                Mensaje = $"Suscripción exitosa al fondo {fondo.Nombre}",
                SaldoActual = cliente.Saldo
            };
        }

        public async Task<CancelacionResponse> CancelarAsync(CancelacionRequest request)
        {
            var cliente = await _clienteRepository.GetByIdAsync(request.IdCliente);
            var fondo = await _fondosRepository.GetByIdAsync(request.IdFondo);

            if (cliente == null || fondo == null)
                throw new InvalidOperationException("Cliente o fondo no encontrado.");

            // Buscar transacción activa (suscripción previa)
            var ultimaSuscripcion = await _transaccionRepository.ObtenerUltimaSuscripcionAsync(request.IdCliente, request.IdFondo);

            if (ultimaSuscripcion == null)
                throw new InvalidOperationException($"No existe una suscripción activa al fondo {fondo.Nombre}");

            // Devolver dinero al cliente
            cliente.Saldo += ultimaSuscripcion.Monto;
            await _clienteRepository.UpdateSaldoAsync(cliente.Id, cliente.Saldo);

            // Registrar transacción de cancelación
            var cancelacion = new Transaccion
            {
                Id = Guid.NewGuid().ToString(),
                IdCliente = cliente.Id,
                IdFondo = fondo.Id,
                Tipo = "Cancelación",
                Monto = ultimaSuscripcion.Monto,
                Fecha = DateTime.UtcNow
            };

            await _transaccionRepository.InsertAsync(cancelacion);

            return new CancelacionResponse
            {
                IdTransaccion = cancelacion.Id,
                Mensaje = $"Suscripción al fondo {fondo.Nombre} cancelada. Monto devuelto: {ultimaSuscripcion.Monto:C}",
                SaldoActual = cliente.Saldo
            };
        }

        public async Task<List<HistorialTransaccionResponse>> ObtenerHistorialTransaccionesAsync(string idCliente)
        {
            var cliente = await _clienteRepository.GetByIdAsync(idCliente);

            if (cliente == null)
                throw new InvalidOperationException("Cliente no encontrado.");

            var transacciones = await _transaccionRepository.GetByClienteAsync(idCliente);

            var respuesta = new List<HistorialTransaccionResponse>();

            foreach (var t in transacciones)
            {
                var fondo = await _fondosRepository.GetByIdAsync(t.IdFondo);

                respuesta.Add(new HistorialTransaccionResponse
                {
                    IdTransaccion = t.Id,
                    NombreFondo = fondo?.Nombre ?? "Desconocido",
                    Tipo = t.Tipo,
                    Monto = t.Monto,
                    Fecha = t.Fecha
                });
            }

            return respuesta.OrderByDescending(x => x.Fecha).ToList();
        }
    }
}
