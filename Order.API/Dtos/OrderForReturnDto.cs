namespace Order.API.Dtos
{
    public class OrderForReturnDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string? Prudoct { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
