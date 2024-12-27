using System.ComponentModel.DataAnnotations;

namespace Order.API.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get;  set; }
        public decimal Price { get; set; }
        public string? Prudoct { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CustomerEmail { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
        }
    }
}
