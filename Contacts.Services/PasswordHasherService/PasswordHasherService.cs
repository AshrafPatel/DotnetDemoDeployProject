using Contacts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Contacts.Services.PasswordHasherService
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string Hash(string password, User user)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool Verify(string password, string hash, User user)
        {
            return _hasher.VerifyHashedPassword(user, hash, password) == PasswordVerificationResult.Success;
        }
    }
}
