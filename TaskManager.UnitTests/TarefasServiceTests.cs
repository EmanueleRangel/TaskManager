using Moq;
using System;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models.InputModels;
using TaskManager.API.Services.Tarefas;
using Xunit;

namespace TaskManager.UnitTests
{
    public class TarefasServiceTests
    {
        
        private readonly TarefasService  _tarefasService;

        public TarefasServiceTests()
        {
            _tarefasService = new TarefasService(new Mock<ITarefasRepository>().Object);
        }

        [Fact]
        public void When_GetById_Given_That_Empty_Guid_Informed_Should_Throws_Exception()
        {
            var exception = Assert.Throws<Exception>(() => _tarefasService.Get(""));
            Assert.Equal("Tarefa não encontrada", exception.Message);
        }

        [Fact]
        public void When_Put_Given_That_Empty_Guid_Informed_Should_Throws_Exception()
        {
            var exception = Assert.Throws<Exception>(() => _tarefasService.Put("", new TarefaInputModel()));
            Assert.Equal("Tarefa não encontrada", exception.Message);
        }

        [Fact]

        public void When_Delete_Given_That_Empty_Guid_Informed_Should_Throws_Exception()
        {
            var exception = Assert.Throws<Exception>(() => _tarefasService.Delete(""));
            Assert.Equal("Tarefa não encontrada", exception.Message);
        }

        [Fact]

        public void When_Post_Given_That_Valid_Objects_Was_Informed_Should_Create_A_New_Task()
        {
            var result = _tarefasService.Post(new TarefaInputModel { Nome = "Tarefa 1", Detalhes = "ir à academia", Concluido = false });
            Assert.True(result);

        }
    }
}