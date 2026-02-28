using Contacts.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.TokenService
{
    public interface ITokenService
    {
        public string GenerateToken(UserProfileDto userProfileDto);
    }
}
