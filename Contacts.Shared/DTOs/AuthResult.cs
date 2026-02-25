using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Shared.DTOs
{
    public class AuthResult
    {
        public bool Success { get; init; }
        public string? AccessToken { get; set; }
        public string? Error { get; set; }  
    }
}
