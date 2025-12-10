using Contacts.Data.Models;

namespace Contacts.Services.Contacts
{
    public interface IContactRepository
    {
        Task<Contact> AddAsync(Contact contact);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Contact>> GetAllContacts();
        Task<Contact?> GetContactAsync(Guid id);
        Task<Contact?> UpdateAsync(Guid id, Contact contact);
        Task<List<Contact>> FindContactByName(string name);
    }
}
