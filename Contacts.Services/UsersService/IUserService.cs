using Contacts.Shared.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.UserService
{
    public interface IUserService
    {
        Task<AuthResult> RegisterUserAsync(RegisterRequestDto registerRequestDto);
        Task CreateJwt(RegisterRequestDto registerRequestDto);
        Task<AuthResult> LoginUserAsync(LoginRequestDto loginRequestDto);
    }
}
