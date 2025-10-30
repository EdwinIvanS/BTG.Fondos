using BTG.Fondos.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DAL.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByUsernameAsync(string username);
        Task CrearUsuarioAsync(Usuario usuario);
    }
}
