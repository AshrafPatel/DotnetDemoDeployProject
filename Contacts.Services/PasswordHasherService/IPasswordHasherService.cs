using Contacts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.PasswordHasherService
{
    public interface IPasswordHasherService
    {
        string Hash(string password, User user);
        bool Verify(string password, string hash, User user);
    }
}
