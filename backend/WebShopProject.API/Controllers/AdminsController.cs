using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopProject.Application.Admins.Commands.CreateAdmin;
using WebShopProject.Application.Admins.Commands.DeleteAdmin;
using WebShopProject.Application.Admins.Commands.UpdateAdmin;
using WebShopProject.Application.Admins.Dto;
using WebShopProject.Application.Admins.Queries.GetAdminById;
using WebShopProject.Application.Admins.Queries.GetAdminByUsername;
using WebShopProject.Application.Admins.Queries.GetAllAdmins;
using WebShopProject.Application.Users.Dto;

namespace WebShopProject.API.Controllers
{
    [ApiController]
    [Route("/admins")]
    [Authorize(Roles = "Admin")]
    public class AdminsController(IMediator mediator): ControllerBase
    {
       

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateAdminCommand command)
        {
            var adminId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = adminId }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            await mediator.Send(new DeleteAdminCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAdmins([FromBody] UpdateAdminCommand command, Guid id)
        {
            command.AdminId = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserDto>> GetByUsername(string username)
        {
            var admin = await mediator.Send(new GetAdminByUsernameQuery(username));
            return Ok(admin);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAllAdmins()
        {
            var admins = await mediator.Send(new GetAllAdminsQuery());
            if (admins == null || !admins.Any())
            {
                return NoContent();
            }
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> GetById(Guid id)
        {
            var admin = await mediator.Send(new GetAdminByIdQuery(id));

            return Ok(admin);
        }
    }
}
