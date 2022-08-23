using System;
using Xunit;
using System.Threading.Tasks;
using Moq;
using Test.Controllers;
using Test.Repositories;
namespace Test.Tests
{
     public class UnitTest1
    {                 
       [Fact]
        public async Task SuccesfullRetrieval()
        {
            Advices result = new Advices();
            var _mockRepo = new Mock<IAdvicesRepository>();
            _mockRepo.Setup(repo => repo.GetAdvices("car",null)).Returns(Task.FromResult(result));
            var controller = new AdvicesController(_mockRepo.Object);
            var resultController = await controller.Get("car",null);
            Assert.Equal(result,resultController);
        }
    }
}
