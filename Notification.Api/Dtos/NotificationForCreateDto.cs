namespace Notification.API.Dtos
{
    public class NotificationForCreateDto
    {
        public Guid IdOrder { get; set; }
        public decimal AmountOrder { get; set; }
        public string? CurrencyOrder { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
