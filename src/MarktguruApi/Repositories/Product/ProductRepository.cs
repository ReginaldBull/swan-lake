namespace MarktguruApi.Repositories.Product
{
    using AutoMapper;
    using Base.Implementations;
    using Base.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Product;
    using Models.Product.Dtos;

    internal sealed class ProductRepository(ApplicationDbContext context, IMapper mapper)
        : EfRepository<Product, CreateProductDto>(context, mapper), IProductRepository
    {
        protected override Task<bool> HasExisting(CreateProductDto createDto, CancellationToken cancellationToken = default) =>
            Context.Set<Product>().AsNoTracking().AnyAsync(p => p.Name == createDto.Name, cancellationToken);
    }
}