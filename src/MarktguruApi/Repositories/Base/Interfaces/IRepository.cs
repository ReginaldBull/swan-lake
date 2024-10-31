namespace MarktguruApi.Repositories.Base.Interfaces
{
    using Models;
    using Models.Results;

    /// <summary>
    /// Defines the basic repository interface for CRUD operations.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TCreateDto">The type of the DTO used for creating a model.</typeparam>
    /// <typeparam name="TUpdateDto">The type of the DTO used for updating a model.</typeparam>
    public interface IRepository<TModel, in TCreateDto, in TUpdateDto> where TModel : IBaseObject
    {
        /// <summary>
        /// Asynchronously creates a new model.
        /// </summary>
        /// <param name="createDto">The DTO containing the data to create the model.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the create operation.</returns>
        Task<CreateResult<TModel>> CreateAsync(TCreateDto createDto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves all models.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of all models.</returns>
        Task<List<TModel>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves a model by its ID.
        /// </summary>
        /// <param name="id">The ID of the model to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the model if found, otherwise null.</returns>
        Task<TModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously updates a model.
        /// </summary>
        /// <param name="id">The ID of the model to update.</param>
        /// <param name="createDto">The DTO containing the data to update the model.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the update operation.</returns>
        Task<UpdateResult<TModel>> UpdateAsync(Guid id, TUpdateDto createDto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously deletes a model by its ID.
        /// </summary>
        /// <param name="id">The ID of the model to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}