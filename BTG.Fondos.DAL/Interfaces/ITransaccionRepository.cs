using BTG.Fondos.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DAL.Interfaces
{
    public interface ITransaccionRepository
    {
        Task InsertAsync(Transaccion transaccion);
        Task<Transaccion?> ObtenerUltimaSuscripcionAsync(string idCliente, string idFondo);
        Task<List<Transaccion>> GetByClienteAsync(string idCliente);
    }
}
