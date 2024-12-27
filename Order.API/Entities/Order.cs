using System.ComponentModel.DataAnnotations;

namespace Order.API.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get;  set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public string? CustomerEmail { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
        }
    }
}
