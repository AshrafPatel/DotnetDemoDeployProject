using Contacts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.Interfaces
{
    internal interface ITokenService
    {
        string GenerateToken(User user);
    }
}
