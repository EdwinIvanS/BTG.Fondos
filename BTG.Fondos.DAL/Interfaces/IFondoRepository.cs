using BTG.Fondos.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DAL.Interfaces
{
    public interface IFondoRepository
    {
        Task<Fondo> GetByIdAsync(string id);
    }
}
