using BTG.Fondos.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.BLL.Services
{
    public class NotificationService : INotificationService
    {
        public async Task EnviarEmailAsync(string destino, string asunto, string cuerpo)
        {
            await Task.Run(() => Console.WriteLine($"Email a {destino}: {asunto} - {cuerpo}"));
        }

        public async Task EnviarSmsAsync(string telefono, string mensaje)
        {
            await Task.Run(() => Console.WriteLine($"SMS a {telefono}: {mensaje}"));
        }
    }
}
