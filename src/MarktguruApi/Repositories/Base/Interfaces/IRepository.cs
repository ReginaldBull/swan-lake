namespace MarktguruApi.Repositories.Base.Interfaces
{
    using Models;
    using Models.Results;

    public interface IRepository<TModel, in TCreateDto, in TUpdateDto> where TModel : IBaseObject
    {
        Task<CreateResult<TModel>> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default);
        Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UpdateResult<TModel>> UpdateAsync(Guid id, TUpdateDto createDto, CancellationToken cancellationToken = default);
    }
}