using Contacts.Services.ContactsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.API.Controllers
{
    private readonly IUserService _userService;
    private readonly ILogger<ProfileController> _logger;


    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetProfile()
        {
            return Ok(new
            {
                message = "You are logged in",
                user = User.Name
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Welcome Admin 👑");
        }
    }
}
