using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Contacts.API.Controllers
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

    }
}
