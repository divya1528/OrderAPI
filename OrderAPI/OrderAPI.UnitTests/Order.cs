using ApplicationCore.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OrderAPI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAPI.UnitTests
{
    public class Tests
    {
        Mock<IOrderRepository> _mockOrderRepository;
        Mock<IUserRepository> _mockUserRepository;
        Mock<ILogger<OrderController>> _mockLogger;
        [SetUp]
        public void Setup()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger<OrderController>>();
        }

        [Test]
        public void TestGetOrders()
        {
            IEnumerable<Order> list = new List<Order>() { new Order() { Id = "1" } };
            _mockOrderRepository.Setup(x=> x.GetOrders()).Returns(Task.FromResult(list));
            OrderController controller = new OrderController(_mockOrderRepository.Object, _mockUserRepository.Object, null, _mockLogger.Object);
            var response = (ObjectResult)controller.Get().Result.Result;
            Assert.AreEqual(response.StatusCode, 200);
        }
    }
}