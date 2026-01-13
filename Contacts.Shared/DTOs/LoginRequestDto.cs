using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Shared.DTOs
{
    public class LoginRequestDto
    {
        [EmailAddress]
        public string Email { get; init; } = null!;
        [Required]
        public string Password { get; init; } = null!;
    }
}
