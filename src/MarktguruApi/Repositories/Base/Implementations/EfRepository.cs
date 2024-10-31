namespace MarktguruApi.Repositories.Base.Implementations
{
    using AutoMapper;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Models;
    using Models.Results;

    /// <summary>
    /// Abstract base class for an Entity Framework repository.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TCreateDto">The type of the DTO used for creating the model.</typeparam>
    /// <typeparam name="TUpdateDto">The type of the DTO used for updating the model.</typeparam>
    public abstract class EfRepository<TModel, TCreateDto, TUpdateDto>(DbContext context, IMapper mapper) : IRepository<TModel, TCreateDto, TUpdateDto>
        where TModel : class, IBaseObject
    {
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly DbContext Context = context;

        /// <summary>
        /// The mapper to convert between models and DTOs.
        /// </summary>
        protected readonly IMapper Mapper = mapper;

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="createDto">The DTO containing the creation details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the creation result.</returns>
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

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities.</returns>
        public Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken = default) =>
            Context.Set<TModel>().AsNoTracking().ToListAsync(cancellationToken);

        /// <summary>
        /// Checks if an entity already exists.
        /// </summary>
        /// <param name="createDto">The DTO containing the creation details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the entity exists.</returns>
        protected abstract Task<bool> HasExisting(TCreateDto createDto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found, otherwise null.</returns>
        public Task<TModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Context.Set<TModel>().AsNoTracking().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be updated.</param>
        /// <param name="createDto">The DTO containing the update details.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the update result.</returns>
        public async Task<UpdateResult<TModel>> UpdateAsync(Guid id, TUpdateDto createDto, CancellationToken cancellationToken = default)
        {
            TModel? entry = await GetByIdWithTrackingAsync(id, cancellationToken).ConfigureAwait(false);
            if (entry == null)
            {
                return UpdateResult<TModel>.CreateFailure("Entity not found.");
            }

            try
            {
                if (HasConflict(entry, createDto))
                {
                    return UpdateResult<TModel>.CreateFailure("Entity has been modified by another user.");
                }

                Context.Set<TModel>().Update(UpdateModel(entry, createDto));
                int affectedRows = await Context.SaveChangesAsync(cancellationToken);
                return affectedRows == 0
                    ? UpdateResult<TModel>.CreateFailure("Failed to save entity.")
                    : UpdateResult<TModel>.CreateSuccess(entry);
            }
            catch (Exception e)
            {
                return UpdateResult<TModel>.CreateFailure(e.Message);
            }
        }

        /// <summary>
        /// Gets an entity by its ID with tracking asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found, otherwise null.</returns>
        private Task<TModel?> GetByIdWithTrackingAsync(Guid id, CancellationToken cancellationToken = default) =>
            Context.Set<TModel>().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

        /// <summary>
        /// Updates the model with the provided update details.
        /// </summary>
        /// <param name="entry">The existing entity.</param>
        /// <param name="updateDto">The DTO containing the update details.</param>
        /// <returns>The updated entity.</returns>
        internal abstract TModel UpdateModel(TModel entry, TUpdateDto updateDto);

        /// <summary>
        /// Checks if there is a conflict between the existing entity and the update details.
        /// </summary>
        /// <param name="entry">The existing entity.</param>
        /// <param name="createDto">The DTO containing the update details.</param>
        /// <returns>A boolean indicating whether there is a conflict.</returns>
        protected abstract bool HasConflict(TModel entry, TUpdateDto createDto);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            TModel? entry = GetByIdAsync(id, cancellationToken).Result;
            if (entry == null)
            {
                return await Task.FromResult(false);
            }

            Context.Set<TModel>().Remove(entry);
            int affectedRows = await Context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0;
        }
    }
}