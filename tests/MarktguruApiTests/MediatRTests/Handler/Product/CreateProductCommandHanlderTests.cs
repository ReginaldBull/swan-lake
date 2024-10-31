namespace MarktguruApiTests.MediatRTests.Handler.Product
{
    using AutoMapper;
    using FluentAssertions;
    using MarktguruApi.MediatR.Commands;
    using MarktguruApi.MediatR.Handlers;
    using MarktguruApi.Models.Product;
    using MarktguruApi.Models.Product.Dtos;
    using MarktguruApi.Models.Results;
    using MarktguruApi.Repositories.Base.Interfaces;
    using Microsoft.Extensions.Logging;
    using Moq;
    [TestFixture]
    public class CreateProductCommandHanlderTests
    {
        [Test]
        public async Task Handle_Tests()
        {
            // Arrange
            var createProductDto = new CreateProductDto("Test Product", 10.0m, true, "Test Description");

            var expectedResult = CreateResult<Product>.CreateSuccess(new Product
            {
                Name = "Test Product",
                Price = 10.0m,
                Available = true,
                Description = "Test Description",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Id = Guid.NewGuid()
            });
            var repository = new Mock<IProductRepository>();
            repository.Setup(p => p.CreateAsync(createProductDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);
            IMapper? mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductResponseDto>();
            }).CreateMapper();
            var logger = Mock.Of<ILogger<CreateProductCommandHandler>>();

            var command = new CreateProductCommand(createProductDto, Guid.NewGuid());
            var handler = new CreateProductCommandHandler(repository.Object, mapper, logger);

            // Act
            var result = await handler.Handle(command, It.IsAny<CancellationToken>());
            
            // Assert
            result.Should().NotBeNull();
        }

        [Test]
        public async Task Handle_NotSuccessful_Tests()
        {
            // Arrange
            var createProductDto = new CreateProductDto("Test Product", 10.0m, true, "Test Description");

            var expectedResult = CreateResult<Product>.CreateFailure("Failed to create product");
            var repository = new Mock<IProductRepository>();
            repository.Setup(p => p.CreateAsync(createProductDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);
            IMapper? mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductResponseDto>();
            }).CreateMapper();
            var logger = Mock.Of<ILogger<CreateProductCommandHandler>>();

            var command = new CreateProductCommand(createProductDto, Guid.NewGuid());
            var handler = new CreateProductCommandHandler(repository.Object, mapper, logger);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => handler.Handle(command, It.IsAny<CancellationToken>()));
        }
    }
}