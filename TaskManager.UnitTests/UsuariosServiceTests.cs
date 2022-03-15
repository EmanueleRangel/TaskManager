﻿using NSubstitute;
using System;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models;
using TaskManager.API.Models.InputModels;
using TaskManager.API.Services.Usuarios;
using Xunit;

namespace TaskManager.UnitTests
{
    public class UsuariosServiceTests
    {
        private IUsuariosRepository _usuariosRepository;
        private readonly UsuariosService _usuariosService;

        
        public UsuariosServiceTests()
        {
            _usuariosRepository = Substitute.For<IUsuariosRepository>();
            _usuariosService = new UsuariosService(_usuariosRepository);
        }

        [Fact]
        public void When_GetById_Given_That_Empty_Guid_Informed_Should_Throws_Exception()
        {
            var exception = Assert.Throws<Exception>(() => _usuariosService.Get(""));
            Assert.Equal("Usuário não encontrado", exception.Message);
        }

        [Fact]

        public void When_Delete_Given_That_Empty_Guid_Informed_Should_Throws_Exception()
        {
            var exception = Assert.Throws<Exception>(() => _usuariosService.Delete(""));
            Assert.Equal("Usuário não encontrado", exception.Message);
        }

        [Fact]

        public async System.Threading.Tasks.Task When_Authentication_Given_That_An_Invalid_Password_Was_Informed_Should_Throws_Exception()
        {
            //Arrange
            var senhaMock = BCrypt.Net.BCrypt.HashPassword("123");
            _usuariosRepository.BuscarUsuarioPorNome(Arg.Any<string>()).Returns(new Usuario { Nome = "Manu", Senha = senhaMock });

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _usuariosService.Authenticate(new LoginInputModel { Nome = "Manu", Senha = "1234" }));

            //Assert
            Assert.Equal("Usuário ou senha inválidos", exception.Message);
        }

        [Fact]

        public void When_Post_Given_That_Valid_Objects_Was_Informed_Should_Create_A_New_User()
        {

            var result = _usuariosService.Post(new UsuarioInputModel { Nome = "Manu", Senha = "manu", Role = "gerente" });
            Assert.True(result);
        }
    }
}