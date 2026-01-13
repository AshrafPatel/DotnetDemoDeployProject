using Contacts.Core.Entities;
using Contacts.Core.Interfaces;
using Contacts.Services.Exceptions;
using Contacts.Services.PasswordHasherService;
using Contacts.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRespository;
        private readonly IPasswordHasherService _passwordHasherService;

        public AuthService(IUserRepository userRepository, IPasswordHasherService passwordHasher)
        {
            _userRespository = userRepository;
            _passwordHasherService = passwordHasher;
        }


        public async Task CreateJwt(RegisterRequestDto registerRequestDto)
        {
            
        }

        public Task<LoginResult> LoginUserAsync(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            var hash = _passwordHasherService.Hash(registerRequestDto.Password, null!);

            if (await _userRespository.GetByEmailAsync(registerRequestDto.Email) != null)
                throw new DuplicateEmailException(registerRequestDto.Email);

            var user = User.Create(registerRequestDto.Email,hash);

            await _userRespository.AddAsync(user);
        }
    }
}
