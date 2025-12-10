using Contacts.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
        : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
