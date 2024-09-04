using KafkaOrders.Models;
using KafkaOrders.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafkaOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly ICustomerOrdersService _customerOrdersService;

        public CustomerOrdersController(ICustomerOrdersService customerOrdersService)
        {
            _customerOrdersService = customerOrdersService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CustomerOrder order)
        {
            await _customerOrdersService.CreateOrderAsync(order);
            return Ok("Order created!");
        }
    }
}
