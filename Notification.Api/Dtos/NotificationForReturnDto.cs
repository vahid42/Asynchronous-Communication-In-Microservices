namespace Notification.API.Dtos
{
    public class NotificationForReturnDto
    {
        public Guid Id { get; set; }
        public string? OrderDeatils { get; set; }
        public DateTime SendDateTime { get; set; }
        public string? Email { get; set; }
    
    }
}
