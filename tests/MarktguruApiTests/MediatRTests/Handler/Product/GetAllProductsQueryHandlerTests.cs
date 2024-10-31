namespace MarktguruApiTests.MediatRTests.Handler.Product
{
    using AutoMapper;
    using FluentAssertions;
    using MarktguruApi.MediatR.Queries;
    using MarktguruApi.MediatR.Handlers;
    using MarktguruApi.Models.Product;
    using MarktguruApi.Models.Product.Dtos;
    using MarktguruApi.Repositories.Base.Interfaces;
    using Moq;

    [TestFixture]
    public class GetAllProductsQueryHandlerTests
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

            var query = new GetAllProductsQuery();
            var repository = new Mock<IProductRepository>();
            IMapper? mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductReducedDto>();
            }).CreateMapper();
            var handler = new GetAllProductsQueryHandler(repository.Object, mapper);

            repository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync([product]);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}