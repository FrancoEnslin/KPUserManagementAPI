using KPUserManagementAPI.BusinessLogic;
using KPUserManagementAPI.Models;
using KPUserManagementAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace KPUserManagementAPI.Tests
{
    public class UsersBusinessLogicTests
    {

        [Fact]
        public async Task GetUserById_Returns_OkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Users.Add(new User { UserId = 1, UserName = "testUser", FirstName = "First", LastName = "Last" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var businessLogic = new UsersBusinessLogic(context);
                var result = await businessLogic.GetUserById(1);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
               
            }
        }

        [Fact]
        public async Task CreateUser_Returns_OkResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var businessLogic = new UsersBusinessLogic(context);
                var newUser = new AddUser { UserName = "newUser", FirstName = "First", LastName = "Last", GroupId = 1 };

                // Act
                var result = await businessLogic.CreateUser(newUser);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("User successfully created", okResult.Value);
            }
        }

    }

}
