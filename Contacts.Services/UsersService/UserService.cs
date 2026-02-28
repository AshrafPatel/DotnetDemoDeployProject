using Contacts.Core.Entities;
using Contacts.Core.Interfaces;
using Contacts.Services.Exceptions;
using Contacts.Services.PasswordHasherService;
using Contacts.Services.TokenService;
using Contacts.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRespository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IPasswordHasherService passwordHasher, ITokenService tokenService)
        {
            _userRespository = userRepository;
            _passwordHasherService = passwordHasher;
            _tokenService = tokenService;
        }


        public async Task CreateJwt(RegisterRequestDto registerRequestDto)
        {
            
        }

        public async Task<AuthResult> LoginUserAsync(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult> RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            string hash = string.Empty;

            if (registerRequestDto.Password != null)
                _passwordHasherService.Hash(registerRequestDto.Password, null!);

            if (registerRequestDto.Name == null)
                throw new ArgumentNullException(nameof(registerRequestDto.Name));

            if (registerRequestDto.Email == null)
                throw new ArgumentNullException(nameof(registerRequestDto.Email));

            if (await _userRespository.GetByEmailAsync(registerRequestDto.Email) != null)
                throw new DuplicateEmailException(registerRequestDto.Email);

            var user = User.Create(registerRequestDto.Name, registerRequestDto.Email,hash);

            await _userRespository.AddAsync(user);
            UserProfileDto userProfileDto = new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString()
            };
            var token = _tokenService.GenerateToken(userProfileDto);
            return new AuthResult { AccessToken = token, Success = true };
        }
    }
}
