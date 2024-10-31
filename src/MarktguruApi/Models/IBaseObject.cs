namespace MarktguruApi.Models
{
    /// <summary>
    /// Represents the base object interface.
    /// </summary>
    public interface IBaseObject
    {
        /// <summary>
        /// Gets the unique identifier of the object.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets the creation date and time of the object.
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated date and time of the object.
        /// </summary>
        DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the version of the object.
        /// </summary>
        int Version { get; set; }
    }
}