using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using Contacts.Core.Entities;
using Contacts.Core.Interfaces;

namespace Contacts.Services.ContactsService
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactService> _logger;
        

        public ContactService(IContactRepository contactRepository, ILogger<ContactService> logger)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<bool> AddAsync(Contact contact)
        {
            try
            {
                _logger.LogInformation("Attempting to add a new contact.");
                if (contact == null)
                {
                    _logger.LogWarning("ContactDto provided is null.");
                    return false;
                }
                await _contactRepository.AddAsync(contact);
                _logger.LogInformation("Successfully added a new contact with ID {ContactId}.", contact.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new contact.");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete contact with ID {ContactId}.", id);
                if (id == Guid.Empty)
                {
                    _logger.LogWarning("Invalid Contact ID provided: {ContactId}.", id);
                    throw new ArgumentException("Invalid contact ID.", nameof(id));
                }
                Contact? contact = await _contactRepository.GetContactAsync(id);
                if (contact == null)
                {
                    _logger.LogWarning("Contact with ID {ContactId} not found.", id);
                    throw new KeyNotFoundException($"Contact with ID {id} not found.");
                }
                await _contactRepository.DeleteAsync(contact.Id);
                _logger.LogInformation("Successfully deleted contact with ID {ContactId}.", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting contact with ID {ContactId}.", id);
                throw;
            }
        }

        public async Task<List<Contact>> FindContactByName(string name)
        {
            List<Contact>? contacts = null;
            try
            {
                _logger.LogInformation("Searching for contacts with name: {ContactName}.", name);
                if (string.IsNullOrWhiteSpace(name)) return new List<Contact>();

                List<Contact> contactToFind = await _contactRepository.FindContactByName(name);
                if (contactToFind == null || contactToFind.Count == 0) return new List<Contact>();
                return contactToFind;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for contacts with name: {ContactName}.", name);
                throw;
            }
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            List<Contact>? contactDtos = null;
            try
            {
                _logger.LogInformation("Retrieving all contacts.");
                List<Contact> contacts = await _contactRepository.GetAllContacts();
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all contacts.");
                throw;
            }
        }

        public async Task<Contact?> GetContactAsync(Guid id)
        {
            Contact? contactToGet = null;
            try
            {
                _logger.LogInformation("Retrieving contact with ID {ContactId}.", id);
                if (id == Guid.Empty) throw new ArgumentException("Invalid contact ID.", nameof(id));

                contactToGet = await _contactRepository.GetContactAsync(id);
                if (contactToGet == null) throw new KeyNotFoundException($"Contact with id {id} not found.");
                return contactToGet;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving contact with ID {ContactId}.", id);
                throw;
            }
        }

        public async Task<Contact?> UpdateAsync(Guid id, Contact contactDto)
        {
            Contact? updateContactDto = null;
            try
            {
                _logger.LogInformation("Attempting to update contact with ID {ContactId}.", id);
                if (id == Guid.Empty) throw new ArgumentException("Invalid contact ID.", nameof(id));
                if (contactDto == null) throw new ArgumentNullException(nameof(contactDto));

                Contact? updatedContact = await _contactRepository.UpdateAsync(id, contactDto);

                return updateContactDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating contact with ID {ContactId}.", id);
                throw;
            }
        }
    }
}
