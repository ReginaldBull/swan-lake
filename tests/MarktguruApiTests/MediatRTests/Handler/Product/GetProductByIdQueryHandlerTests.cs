namespace MarktguruApiTests.MediatRTests.Handler.Product
{
    using AutoMapper;
    using FluentAssertions;
    using MarktguruApi.MediatR.Handlers;
    using MarktguruApi.MediatR.Queries;
    using MarktguruApi.Models.Product;
    using MarktguruApi.Models.Product.Dtos;
    using MarktguruApi.Repositories.Base.Interfaces;
    using Moq;

    [TestFixture]
    public class GetProductByIdQueryHandlerTests
    {
        [Test]
        public async Task Handle_Test()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Price = 10.0m,
                Available = true,
                Description = "Test Description",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var query = new GetProductByIdQuery(product.Id);
            var repository = new Mock<IProductRepository>();
            IMapper? mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductResponseDto>();
            }).CreateMapper();
            var handler = new GetProductByIdQueryHandler(repository.Object, mapper);

            repository.Setup(x => x.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.Name.Should().Be(product.Name);
            result.Price.Should().Be(product.Price);
        }
    }
}