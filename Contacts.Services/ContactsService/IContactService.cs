
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Services.ContactsService
{
    public interface IContactService
    {
        Task<bool> AddAsync(Contact contact);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Contact>> GetAllContacts();
        Task<Contact?> GetContactAsync(Guid id);
        Task<Contact?> UpdateAsync(Guid id, Contact contact);
        Task<List<Contact>> FindContactByName(string name);
    }
}
