using System.ComponentModel.DataAnnotations;

namespace Notification.API.Entities
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        public string? OrderDeatils { get; set; }
        public DateTime SendDateTime { get; set; }
        public string? Email { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid();
        }
    }
}
