using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notification.API.Dtos;
using Notification.API.Services;

namespace Notification.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService,
            ILogger<NotificationController> logger,
            IMapper mapper)
        {
            _notificationService = notificationService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationForReturnDto>>> GetNotifications()
        {
            var notification = await _notificationService.GetNotificationsAsync();
            return Ok(_mapper.Map<IEnumerable<NotificationForReturnDto>>(notification));
        }

        [HttpGet("notificationId}", Name = nameof(GetNotification))]
        public async Task<IActionResult> GetNotification(Guid notificationId)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(notificationId);
            return Ok(_mapper.Map<NotificationForReturnDto>(notification));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotifications([FromBody] NotificationForCreateDto notificationForCreateDto)
        {
            var notification = await _notificationService.CreateNotificationAsync(_mapper.Map<Entities.Notification>(notificationForCreateDto));
            var notificationDto = _mapper.Map<NotificationForReturnDto>(notification);
            return CreatedAtAction(nameof(GetNotification), new { notificationId = notification.Id }, new { notificationDto });
        }

    }
}