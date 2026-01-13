using Contacts.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Core.Entities
{
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [EmailAddress]
        [Column("email")]
        [Required]
        public string Email { get; set; } = "";
        [Column("password_hash")]
        [Required]
        public string PasswordHash { get; set; } = "";
        [Column("role")]
        public string Role { get; set; } = "User";

        private User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public static User Create(
        string email,
        string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Password hash is required");

            return new User(email, passwordHash);
        }
    }
}
