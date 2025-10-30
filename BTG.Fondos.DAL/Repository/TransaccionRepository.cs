using BTG.Fondos.DAL.Interfaces;
using BTG.Fondos.DTO.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DAL.Repository
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly IMongoCollection<Transaccion> _transacciones;

        public TransaccionRepository(IMongoDatabase database)
        {
            _transacciones = database.GetCollection<Transaccion>("Transacciones");
        }

        public async Task InsertAsync(Transaccion transaccion)
        {
            try { 
                await _transacciones.InsertOneAsync(transaccion);
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }

        public async Task<Transaccion?> ObtenerUltimaSuscripcionAsync(string idCliente, string idFondo)
        {
            try { 
                return await _transacciones.Find(t =>
                    t.IdCliente == idCliente &&
                    t.IdFondo == idFondo &&
                    t.Tipo == "Apertura")
                    .SortByDescending(t => t.Fecha)
                    .FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Transaccion>> GetByClienteAsync(string idCliente)
        {
            try { 
                return await _transacciones.Find(t => t.IdCliente == idCliente)
                                           .SortByDescending(t => t.Fecha)
                                           .ToListAsync();
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }
    }
}
