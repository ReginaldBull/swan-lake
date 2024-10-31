namespace MarktguruApi.Repositories.Product
{
    using AutoMapper;
    using Base.Implementations;
    using Base.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Product;
    using Models.Product.Dtos;
    using Utils;

    /// <summary>
    /// Repository for managing product entities.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    internal sealed class ProductRepository(ApplicationDbContext context, IMapper mapper)
        : EfRepository<Product, CreateProductDto, UpdateProductDto>(context, mapper), IProductRepository
    {
        /// <summary>
        /// Checks if a product with the same name already exists in the database.
        /// </summary>
        /// <param name="createDto">The DTO containing the product details to check.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether a product with the same name exists.</returns>
        protected override Task<bool> HasExisting(CreateProductDto createDto, CancellationToken cancellationToken = default) =>
            Context.Set<Product>().AsNoTracking().AnyAsync(p => p.Name == createDto.Name, cancellationToken);

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="mapper">The AutoMapper instance used for mapping entities to DTOs.</param>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="requestPageSize">The number of items per page.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of ProductReducedDto.</returns>
        public async Task<PaginatedList<ProductReducedDto>> GetPaginatedAsync(
            IMapper mapper,
            int pageNumber,
            int requestPageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<Product> query = Context.Set<Product>().AsNoTracking();
            return await PaginatedList<ProductReducedDto>.ToPaginatedList(
                mapper.ProjectTo<ProductReducedDto>(query),
                pageNumber,
                requestPageSize,
                cancellationToken);
        }

        /// <summary>
        /// Updates the properties of an existing product entity with the values from the update DTO.
        /// </summary>
        /// <param name="entry">The product entity to update.</param>
        /// <param name="updateDto">The DTO containing the updated product details.</param>
        /// <returns>The updated product entity.</returns>
        internal override Product UpdateModel(Product entry, UpdateProductDto updateDto)
        {
            entry.Name = updateDto.Name;
            entry.Price = updateDto.Price;
            entry.Description = updateDto.Description;
            entry.Available = updateDto.Available;
            return entry;
        }

        /// <summary>
        /// Checks if there is a version conflict between the existing product entity and the update DTO.
        /// </summary>
        /// <param name="entry">The existing product entity.</param>
        /// <param name="createDto">The DTO containing the updated product details.</param>
        /// <returns>A boolean indicating whether there is a version conflict.</returns>
        protected override bool HasConflict(Product entry, UpdateProductDto createDto) => entry.Version != createDto.Version;
    }
}