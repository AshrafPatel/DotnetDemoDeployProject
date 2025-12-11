
using Contacts.Core.Entities;
using Contacts.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Services.ContactsService
{
    public interface IContactService
    {
        Task<bool> AddAsync(ContactDto contact);
        Task<bool> DeleteAsync(Guid id);
        Task<List<ContactDto>> GetAllContacts();
        Task<ContactDto?> GetContactAsync(Guid id);
        Task<ContactDto?> UpdateAsync(Guid id, ContactDto contact);
        Task<List<ContactDto>> FindContactByName(string name);
    }
}
