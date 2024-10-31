namespace MarktguruApi.Models.Product.Dtos
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public int Version { get; set; }
    }
}