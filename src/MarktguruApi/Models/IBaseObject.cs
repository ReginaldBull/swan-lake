namespace MarktguruApi.Models
{
    internal interface IBaseObject
    {
        Guid Id { get; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        int Version { get; set; }
    }
}