using Contacts.Core.Entities;

namespace Contacts.Core.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> AddAsync(Contact contact);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Contact>> GetAllContacts();
        Task<Contact?> GetContactAsync(Guid id);
        Task<Contact?> UpdateAsync(Guid id, Contact contact);
        Task<List<Contact>> FindContactByName(string name);
        Task<bool> IsEmailExists(string email);
    }
}
