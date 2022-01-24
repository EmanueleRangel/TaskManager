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
        public void GetById_SendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => _tarefasService.Get(""));
            Assert.Equal("Tarefa não encontrada", exception.Message);
        }

        [Fact]
        public void Put_SendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => _tarefasService.Put("", new TarefaInputModel()));
            Assert.Equal("Tarefa não encontrada", exception.Message);
        }

        [Fact]

        public void Delete_SendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => _tarefasService.Delete(""));
            Assert.Equal("Tarefa não encontrada", exception.Message);
        }
    }
}