using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order.API.Dtos;
using Order.API.Messaging;
using Order.API.Services;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _ordersService;
        private readonly ILogger<OrderController> _logger;
        private readonly ISenderMessage senderMessage;
        private readonly IMapper _mapper;

        public OrderController(IOrderService ordersService,
            ILogger<OrderController> logger, ISenderMessage senderMessage,
            IMapper mapper)
        {
            _ordersService = ordersService;
            _logger = logger;
            this.senderMessage = senderMessage;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderForReturnDto>>> GetOrders()
        {
            var orders = await _ordersService.GetOrdersAsync();
            return Ok(_mapper.Map<IEnumerable<OrderForReturnDto>>(orders));
        }

        [HttpGet("{orderId}", Name = nameof(GetOrder))]
        public async Task<IActionResult> GetOrder(Guid orderId)
        {
            var orders = await _ordersService.GetOrderByIdAsync(orderId);
            return Ok(_mapper.Map<OrderForReturnDto>(orders));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrders([FromBody] OrderForCreateDto orderForCreateDto)
        {
            var order = await _ordersService.CreateOrderAsync(_mapper.Map<Entities.Order>(orderForCreateDto));
            var orderDto = _mapper.Map<OrderForReturnDto>(order);
            await senderMessage.SendMessage(order);
            return CreatedAtAction(nameof(GetOrder), new { orderId = order.Id }, new { orderDto });
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrders([FromBody] OrderForUpdateDto orderForUpdateDto, Guid orderId)
        {
            var order = _mapper.Map<Entities.Order>(orderForUpdateDto);
            order.Id = orderId;

            var updateOrder = await _ordersService.UpdateOrderAsync(order);
            var orderDto = _mapper.Map<OrderForReturnDto>(updateOrder);

            return Ok(orderDto);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            var result = await _ordersService.DeleteOrderAsync(orderId);
            return result ? NoContent() : BadRequest();
        }
    }
}