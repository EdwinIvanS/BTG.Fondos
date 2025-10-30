using BTG.Fondos.DTO.Fondos.Request;
using BTG.Fondos.DTO.Fondos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.BLL.Interfaces
{
    public interface IFondoService
    {
        Task<SuscripcionResponse> SuscribirAsync(SuscripcionRequest request);
        Task<CancelacionResponse> CancelarAsync(CancelacionRequest request);
        Task<List<HistorialTransaccionResponse>> ObtenerHistorialTransaccionesAsync(string idCliente);
    }
}
