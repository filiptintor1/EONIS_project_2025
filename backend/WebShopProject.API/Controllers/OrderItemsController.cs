using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebShopProject.Application.OrderItems.Commands.CreateOrderItemCommand;
using WebShopProject.Application.OrderItems.Commands.DeleteOrderItemCommand;
using WebShopProject.Application.OrderItems.Commands.UpdateOrderItemCommand;
using WebShopProject.Application.OrderItems.Dto;
using WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderAndProduct;
using WebShopProject.Application.OrderItems.Queries.GetOrderItemsByOrderId;

namespace WebShopProject.API.Controllers
{
    [ApiController]
    [Route("/order-items")]    
    public class OrderItemsController(IMediator mediator) : ControllerBase
    {

        [HttpGet("by-order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItemsByOrderId(Guid orderId)
        {
            var query = new GetOrderItemsByOrderIdQuery(orderId);
            var orderItems = await mediator.Send(query);

            if (orderItems == null || !orderItems.Any())
            {
                return NotFound();
            }

            return Ok(orderItems);
        }

        [HttpGet("{orderId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderItemByOrderAndProduct(Guid orderId, Guid productId)
        {
            var query = new GetOrderItemsByOrderAndProductQuery(orderId, productId);
            var orderItem = await mediator.Send(query);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(CreateOrderItemCommand command)
        {
            await mediator.Send(command);
            return Ok(command);
        }

        [HttpDelete("{orderId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId,  Guid productId)
        {
            await mediator.Send(new DeleteOrderItemCommand(orderId, productId));

            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrderItem(UpdateOrderItemCommand command)
        {
            await mediator.Send(command);
            return NoContent();


        }
    }
}
