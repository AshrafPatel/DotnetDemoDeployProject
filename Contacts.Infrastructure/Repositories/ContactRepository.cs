using Contacts.Core.Entities;
using Contacts.Core.Interfaces;
using Contacts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contacts.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _contactDbContext;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository(ContactDbContext contactDbContext, ILogger<ContactRepository> logger)
        {
            _contactDbContext = contactDbContext ?? throw new ArgumentNullException(nameof(contactDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }
            contact.Id = Guid.NewGuid();
            await _contactDbContext.AddAsync(contact);
            await _contactDbContext.SaveChangesAsync();
            _logger.LogInformation("Added new contact with ID {ContactId}", contact.Id);
            return contact;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Contact? contact;
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"Invalid contact ID {id}");
            }
            contact = await _contactDbContext.Contacts.SingleOrDefaultAsync(x => x.Id == id);
            if (contact == null) { return false; }
            _contactDbContext.Contacts.Remove(contact);
            await _contactDbContext.SaveChangesAsync();
            _logger.LogInformation("Deleted contact with ID {ContactId}", id);
            return true;

        }

        public async Task<List<Contact>> GetAllContacts()
        {
            _logger.LogInformation("Retrieving all contacts from the database.");
            List<Contact> contacts = await _contactDbContext.Contacts.ToListAsync();
            _logger.LogInformation("Retrieved {Count} contacts from database", contacts.Count);

            return contacts;
        }

        public async Task<Contact?> GetContactAsync(Guid id)
        {
            _logger.LogInformation("Retrieving contact with ID {ContactId} from the database.", id);
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"Invalid contact ID {id}");
            }
            Contact? contact = await _contactDbContext.Contacts.SingleOrDefaultAsync(x => x.Id == id);
            return contact;
        }

        public async Task<Contact?> UpdateAsync(Guid id, Contact contact)
        {
            Contact? contactInDb = null;

            if (id == Guid.Empty)
            {
                throw new ArgumentException($"Invalid contact ID {id}");
            }
            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            contactInDb = await _contactDbContext.Contacts.SingleOrDefaultAsync(x => x.Id == id);
            if (contactInDb == null) return null;
            contactInDb.Name = contact.Name;
            contactInDb.Email = contact.Email;

            await _contactDbContext.SaveChangesAsync();
            _logger.LogInformation("Updated contact with ID {ContactId}", id);
            return contactInDb;
        }

        public async Task<List<Contact>> FindContactByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }
            List<Contact>? contacts = await _contactDbContext.Contacts.Where(x => x.Name.Contains(name)).ToListAsync();
            _logger.LogInformation("{Count} contacts found for name {Name}", contacts.Count, name);
            return contacts;
        }
    }
}
