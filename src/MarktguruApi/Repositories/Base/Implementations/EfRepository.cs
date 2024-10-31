namespace MarktguruApi.Repositories.Base.Implementations
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Models;
    using Models.Results;

    public abstract class EfRepository<TModel, TCreateDto>(DbContext context, IMapper mapper) : IRepository<TModel, TCreateDto>
        where TModel : class, IBaseObject
    {
        protected readonly DbContext Context = context;
        protected readonly IMapper Mapper = mapper;

        public async Task<CreateResult<TModel>> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default)
        {
            try
            {
                bool hasExisting = await HasExisting(createDto, cancellationToken);
                if (hasExisting)
                {
                    return CreateResult<TModel>.CreateFailure("Entity already exists.");
                }
                
                TModel model = Mapper.Map<TCreateDto, TModel>(createDto);
                EntityEntry<TModel> entry = await Context.Set<TModel>()
                    .AddAsync(model, cancellationToken);
                int affectedRows = await Context.SaveChangesAsync(cancellationToken);
                return affectedRows == 0 
                    ? CreateResult<TModel>.CreateFailure("Failed to save entity.") 
                    : CreateResult<TModel>.CreateSuccess(entry.Entity);
            }
            catch (Exception e)
            {
                return CreateResult<TModel>.CreateFailure(e.Message);
            }
        }
        
        protected abstract Task<bool> HasExisting(TCreateDto createDto, CancellationToken cancellationToken = default);

    }
}