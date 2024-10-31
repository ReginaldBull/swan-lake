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
        /*
         * In a real-world scenario, Version would be not generated in code.
         * The definition of it would look more like this:
         * [Timestamp]
         * public byte[] Version { get; set; }
         *
         * The database would generate the value for the Version property.
         *
         * In this scenario and because of usage of a InMemoryDatabase, Version is of type int and generated by code.
         */
        public int Version { get; set; }
    }

}