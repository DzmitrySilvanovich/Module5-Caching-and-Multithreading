using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ticketing.BAL.Contracts;
using Ticketing.BAL.Model;
using Ticketing.BAL.Services;
using Ticketing.DAL.Domain;

namespace Ticketing.UI.Controllers
{
    /// <summary>
    /// Orders API
    /// </summary>
    //  [Route("api/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Release Carts From Order.
        /// <param name="orderId">Cart id</param>
        /// <response code="200">Return a status of request</response>
        /// </summary>
        [HttpDelete("release/{orderId}")]
        public async Task<IActionResult> DeleteAsync(int orderId)
        {
            var result = await _orderService.ReleaseCartsFromOrderAsync(orderId);


            if (!result)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        /// <summary>
        /// create new order
        /// <param name="order">new order</param>
        /// <returns>new order</returns>
        /// <response code="201">Return a status of request with result</response>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            var result = await _orderService.CreateOrder(order);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// create new order
        /// <param name="order">order for update</param>
        /// <returns>new order</returns>
        /// <response code="200">Return a status</response>
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Order order)
        {
            await _orderService.UpdateOrder(order);

            return Ok();
        }
    }
}
