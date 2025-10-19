using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopProject.Application.Orders.Commands.CreateOrderCommand;
using WebShopProject.Application.Orders.Commands.DeleteOrderCommand;
using WebShopProject.Application.Orders.Commands.UpdateOrderCommand;
using WebShopProject.Application.Orders.Dto;
using WebShopProject.Application.Orders.Queries.GetAllOrders;
using WebShopProject.Application.Orders.Queries.GetOrderById;

namespace WebShopProject.API.Controllers
{
    [ApiController]
    [Route("/orders")]
    public class OrdersController(IMediator mediator) :ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await mediator.Send(new GetAllOrdersQuery());
            if (orders == null || !orders.Any())
            {
                return NoContent();
            }
            return Ok(orders);
        }

        [HttpPatch("{orderId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrder(Guid orderId, UpdateOrderCommand command)
        {
            command.OrderId = orderId;
            await mediator.Send(command);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            Guid orderId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { orderId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var user = await mediator.Send(new GetOrderByIdQuery(id));
            return Ok(user);
        }

        

    }
}
