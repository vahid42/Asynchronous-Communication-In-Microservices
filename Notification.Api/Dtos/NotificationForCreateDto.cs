namespace Notification.API.Dtos
{
    public class NotificationForCreateDto
    {
        public Guid IdOrder { get; set; }
        public decimal PriceOrder { get; set; }
        public string? PrudoctOrder { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
