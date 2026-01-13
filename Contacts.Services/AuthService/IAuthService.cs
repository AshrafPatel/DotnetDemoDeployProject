using Contacts.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.AuthService
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterRequestDto registerRequestDto);
        Task CreateJwt(RegisterRequestDto registerRequestDto);
        Task<LoginResult> LoginUserAsync(LoginRequestDto loginRequestDto);
    }
}
