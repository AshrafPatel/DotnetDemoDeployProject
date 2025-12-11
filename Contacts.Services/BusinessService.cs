using Contacts.Services.ContactsService;
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
        }
    }
}
