using Contacts.Services.ContactsService;
using Contacts.Services.PasswordHasherService;
using Contacts.Services.TokenService;
using Contacts.Services.UserService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services
{
    public static class BusinessService
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService.PasswordHasherService>();
            services.AddScoped<IUserService, UserService.UserService>();
            services.AddScoped<ITokenService, TokenService.TokenService>();
        }
    }
}
