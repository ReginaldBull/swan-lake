namespace MarktguruApiTests.ControllerTests.Authentication
{
    using FluentAssertions;
    using MarktguruApi.Controllers.Authentication;
    using MarktguruApi.Models.Authentication;
    using Microsoft.AspNetCore.Mvc;

    [TestFixture]
    public class AuthorizationControllerTests
    {
        [Test]
        public async Task GenerateToken_Test()
        {
            // Arrange
            var controller = new AuthorizationController();

            TokenGenerationDto dto = new TokenGenerationDto
            {
                UserName = "Test",
                Password = "test"
            };
            
            // Act
            var token = await controller.GenerateToken(dto);

            // Assert
            token.Should().BeOfType<OkObjectResult>();
        }
        
        [Test]
        public async Task GenerateToken_WrongCredentials_Test()
        {
            // Arrange
            var controller = new AuthorizationController();

            TokenGenerationDto dto = new TokenGenerationDto
            {
                UserName = "Test1",
                Password = "test1"
            };
            
            // Act
            var token = await controller.GenerateToken(dto);

            // Assert
            token.Should().BeOfType<UnauthorizedResult>();
        }
        
        [Test]
        public async Task GenerateToken_EmptyCredentials_Test()
        {
            // Arrange
            var controller = new AuthorizationController();

            TokenGenerationDto dto = new TokenGenerationDto
            {
                UserName = null,
                Password = "test1"
            };
            
            // Act
            var token = await controller.GenerateToken(dto);

            // Assert
            token.Should().BeOfType<BadRequestResult>();
        }
    }
}