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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioRepository(IMongoDatabase database)
        {
            _usuarios = database.GetCollection<Usuario>("Usuarios");
        }

        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            try { 
                await _usuarios.InsertOneAsync(usuario);
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }

        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            try { 
                return await _usuarios.Find(u => u.NombreUsuario == username).FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                Console.WriteLine($"Error MongoDB: {ex.Message}");
                throw;
            }
        }
    }
}
