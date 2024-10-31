namespace MarktguruApi.Repositories.Base.Interfaces
{
    using Models.Product;
    using Models.Product.Dtos;

    public interface IProductRepository : IRepository<Product, CreateProductDto>;
}