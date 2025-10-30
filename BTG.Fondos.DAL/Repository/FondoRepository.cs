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
    public class FondoRepository : IFondoRepository
    {
        private readonly IMongoCollection<Fondo> _fondos;

        public FondoRepository(IMongoDatabase database)
        {
            _fondos = database.GetCollection<Fondo>("Fondos");
        }

        public async Task<Fondo?> GetByIdAsync(string id)
        {
            try { 
                return await _fondos.Find(f => f.Id == id).FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }
    }
}
