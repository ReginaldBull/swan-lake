namespace MarktguruApi.Repositories.Base.Interfaces
{
    using Models;
    using Models.Results;

    public interface IRepository<TModel, in TCreateDto> where TModel : IBaseObject
    {
        Task<CreateResult<TModel>> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default);
    }
}