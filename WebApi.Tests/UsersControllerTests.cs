using System;
using Xunit;
using WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infrastructure.Repos;
using Moq;
using WebApi.Models;
using System.Collections.Generic;

namespace WebApi.Tests
{
    public class UsersControllerTests
    {

        [Fact]
        public async void Test1()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetUsersAsync().Result).Returns(GetTestUsers());
            var controller = new UsersController(mock.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }
        private IEnumerable<UserWithCompany> GetTestUsers()
        {
            var users = new List<UserWithCompany>
                 {
                     new UserWithCompany { Id=1, Name="Tom", Age=35, Activity = "�������", Company = "bn"},
                     new UserWithCompany { Id=2, Name="Alice", Age=29, Activity = "�������", Company = "bn"},
                     new UserWithCompany { Id=3, Name="Sam", Age=32, Activity = "�������", Company = "bn"},
                     new UserWithCompany { Id=4, Name="Kate", Age=30, Activity = "�������", Company = "bn"}
                 };
            IEnumerable<UserWithCompany> en = users;
            return en;
        }
    }
}
