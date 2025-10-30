using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.BLL.Interfaces
{
    public interface INotificationService
    {
        Task EnviarEmailAsync(string destino, string asunto, string cuerpo);
        Task EnviarSmsAsync(string telefono, string mensaje);
    }
}
