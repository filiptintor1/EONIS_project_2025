using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopProject.Application.Orders.Dto;
using WebShopProject.Application.Orders.Queries.GetOrderByUser;
using WebShopProject.Application.Users.Commands.CreateUser;
using WebShopProject.Application.Users.Commands.DeleteUser;
using WebShopProject.Application.Users.Commands.UpdateUser;
using WebShopProject.Application.Users.Dto;
using WebShopProject.Application.Users.Queries.GetAllUsers;
using WebShopProject.Application.Users.Queries.GetUserById;
using WebShopProject.Application.Users.Queries.GetUserByUsername;

namespace WebShopProject.API.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllUsers()
        {
            var users = await mediator.Send(new GetAllUsersQuery());
            if (users == null || !users.Any())
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserDto>> GetUserByUsername(string username)
        {
            var user = await mediator.Send(new GetUserByUsernameQuery(username));

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            Guid userId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId }, null);
        }

        [HttpGet("{userId}/orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders(Guid userId)
        {
            var orders = await mediator.Send(new GetOrderByUserQuery(userId));
            if (orders == null || !orders.Any())
            {
                return NoContent();
            }
            return Ok(orders);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await mediator.Send(new DeleteUserCommand(userId));

            return NoContent();
        }

        [HttpPatch("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserCommand command)
        {
            command.UserId = userId;
            await mediator.Send(command);
            return NoContent();

        }

    }
}
