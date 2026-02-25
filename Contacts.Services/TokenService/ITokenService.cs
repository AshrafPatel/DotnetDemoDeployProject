using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.TokenService
{
    public interface ITokenService
    {
        public string GenerateToken(string userId, string email, string role);
    }
}
