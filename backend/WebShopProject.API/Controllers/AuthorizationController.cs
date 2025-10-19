
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using WebShopProject.Domain.Authorization;
using WebShopProject.Domain.Entities;

namespace WebShopProject.API.Controllers
{
    [ApiController]
    [Route("/authorization")]
    public class AuthorizationController(IAuthorizationHelper authorizationHelper) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate(Credentials creds)
        {

            if (authorizationHelper.Authenticate(creds))
            {
                var token = authorizationHelper.GenerateToken(creds);
                return Ok(new { token = token, creds.Username });
            }
            return Unauthorized();
        }
    }
}
