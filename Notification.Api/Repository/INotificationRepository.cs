 namespace Notification.API.Repository

{
    public interface INotificationRepository
    {
        Task<Entities.Notification> CreateNotificationAsync(Entities.Notification entity);
        Task<Entities.Notification> GetNotificationByIdAsync(Guid notificationId);
        Task<IEnumerable<Entities.Notification>> GetNotificationsAsync();
    }
}
