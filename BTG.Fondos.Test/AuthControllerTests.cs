using BTG.Fondos.API.Controllers;
using BTG.Fondos.Auth.Token;
using BTG.Fondos.DAL.Interfaces;
using BTG.Fondos.DTO.Auth.Request;
using BTG.Fondos.DTO.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BTG.Fondos.Test
{
    public class AuthControllerTests
    {
        [Fact]
        // ---------- TEST 1: REGISTRO DE USUARIO VALIDO ----------
        public async Task Register_Usuario_Valido()
        {
            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(r => r.GetByUsernameAsync(It.IsAny<string>()))
                    .ReturnsAsync((Usuario)null!); 

            mockRepo.Setup(r => r.CrearUsuarioAsync(It.IsAny<Usuario>()))
                    .Returns(Task.CompletedTask);

            var mockConfig = new Mock<IConfiguration>();
            var tokenService = new TokenService(mockConfig.Object);
            var controller = new AuthController(mockRepo.Object, tokenService);

            var request = new RegisteRequest
            {
                Username = "edwin",
                Password = "1234",
                Email = "edwin@mail.com",
                Rol = "Admin"
            };

            var result = await controller.Register(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Usuario registrado correctamente.", okResult.Value);
        }

        [Fact]
        // ---------- TEST 2: LOGIN CON CREDENCIALES CORRECTAS ----------
        public async Task Login_Credenciales_Correctas()
        {
            var usuario = new Usuario
            {
                NombreUsuario = "admin",
                Rol = "Admin",
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword("1234")
            };

            var mockRepo = new Mock<IUsuarioRepository>();
            mockRepo.Setup(r => r.GetByUsernameAsync("admin")).ReturnsAsync(usuario);

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["JwtSettings:Key"]).Returns("EstaEsUnaClaveSeguraDePruebaConMasDe32Caracteres!!");
            mockConfig.Setup(c => c["JwtSettings:Issuer"]).Returns("BTGAuth");
            mockConfig.Setup(c => c["JwtSettings:Audience"]).Returns("BTGUsers");
            mockConfig.Setup(c => c["JwtSettings:DurationInMinutes"]).Returns("30");

            var tokenService = new TokenService(mockConfig.Object);
            var controller = new AuthController(mockRepo.Object, tokenService);

            var request = new DTO.Auth.Request.LoginRequest
            {
                Username = "admin",
                Password = "1234"
            };

            var result = await controller.Login(request);

            var okResult = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(okResult.Value);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!;

            Assert.True(data.ContainsKey("token"));
            Assert.True(data.ContainsKey("rol"));
            Assert.Equal("Admin", data["rol"].ToString());
            Assert.False(string.IsNullOrEmpty(data["token"].ToString()));
        }
    }
}
