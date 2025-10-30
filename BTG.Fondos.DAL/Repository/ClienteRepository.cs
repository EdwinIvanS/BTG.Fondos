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
    public class ClienteRepository : IClienteRepository
    {
        private readonly IMongoCollection<Cliente> _clientes;

        public ClienteRepository(IMongoDatabase database)
        {
            _clientes = database.GetCollection<Cliente>("Clientes");
        }

        public async Task<Cliente?> GetByIdAsync(string id)
        {
            try
            {
                var filtro = Builders<Cliente>.Filter.Eq("_id", id);
                return await _clientes.Find(filtro).FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateSaldoAsync(string id, decimal nuevoSaldo)
        {
            try
            {
                var update = Builders<Cliente>.Update.Set(c => c.Saldo, nuevoSaldo);
                await _clientes.UpdateOneAsync(c => c.Id == id, update);
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }
    }
}
