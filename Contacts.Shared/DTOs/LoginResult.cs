using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Shared.DTOs
{
    public class LoginResult
    {
        public bool Success { get; init; }
        public string? AccessToken { get; init; }
        public string? Error { get; init; }  

        public static LoginResult Successful(string accessToken) => new LoginResult
        {
            Success = true,
            AccessToken = accessToken,
            Error = null
        };

        public static LoginResult Failed(string error) => new LoginResult
        {
            Success = false,
            AccessToken = null,
            Error = error
        };
    }
}
