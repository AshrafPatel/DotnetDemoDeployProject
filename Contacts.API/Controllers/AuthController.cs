using Contacts.Core.Entities;
using Contacts.Services.UserService;
using Contacts.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Contacts.API.Controllers
{
    /*
     *  User logs in → IAuthService.LoginAsync() validates credentials AND generates token (all in one)
        User accesses their profile → IUserService.GetProfileAsync() (protected route, token already validated by middleware)
     */
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                AuthResult registerToken = await  _userService.RegisterUserAsync(request);
                if (registerToken.Success)
                {
                    return Ok(new { Message = "User registered successfully." });
                }
                else
                {
                    return BadRequest(registerToken.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                AuthResult loginToken = await _userService.LoginUserAsync(request);
                if (loginToken.Success)
                {
                    return Ok(new { Token = loginToken.AccessToken });
                }
                else
                {
                    return Unauthorized(loginToken.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user login.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
            /*var user = db.Users.SingleOrDefault(u =>
                u.Email == request.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid credentials");

            // ✅ Password correct → issue JWT
            var token = CreateJwt(user);
            return Ok(new { token });*/
        }
    }
}
