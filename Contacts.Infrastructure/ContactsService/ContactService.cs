using AutoMapper;
using Contacts.Data.Models;
using Contacts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Contacts.Services.Contacts
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactService> _logger;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, ILogger<ContactService> logger, IMapper mapper)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<bool> AddAsync(ContactDto contactDto)
        {
            try
            {
                _logger.LogInformation("Attempting to add a new contact.");
                if (contactDto == null)
                {
                    _logger.LogWarning("ContactDto provided is null.");
                    return false;
                }
                Contact contactToAdd = _mapper.Map<Contact>(contactDto);
                await _contactRepository.AddAsync(contactToAdd);
                _logger.LogInformation("Successfully added a new contact with ID {ContactId}.", contactToAdd.Id);
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

        public async Task<List<ContactDto>> FindContactByName(string name)
        {
            List<ContactDto>? contactDtos = null;
            try
            {
                _logger.LogInformation("Searching for contacts with name: {ContactName}.", name);
                if (string.IsNullOrWhiteSpace(name)) return new List<ContactDto>();

                List<Contact> contacts = await _contactRepository.FindContactByName(name);
                if (contacts == null || contacts.Count == 0) return new List<ContactDto>();

                contactDtos = _mapper.Map<List<ContactDto>>(contacts);
                return contactDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for contacts with name: {ContactName}.", name);
                throw;
            }
        }

        public async Task<List<ContactDto>> GetAllContacts()
        {
            List<ContactDto>? contactDtos = null;
            try
            {
                _logger.LogInformation("Retrieving all contacts.");
                List<Contact> contacts = await _contactRepository.GetAllContacts();
                contactDtos = _mapper.Map<List<ContactDto>>(contacts);
                return contactDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all contacts.");
                throw;
            }
        }

        public async Task<ContactDto?> GetContactAsync(Guid id)
        {
            ContactDto? contactDto = null;
            try
            {
                _logger.LogInformation("Retrieving contact with ID {ContactId}.", id);
                if (id == Guid.Empty) throw new ArgumentException("Invalid contact ID.", nameof(id));

                Contact? contact = await _contactRepository.GetContactAsync(id);
                if (contact == null) throw new KeyNotFoundException($"Contact with id {id} not found.");

                contactDto = _mapper.Map<ContactDto>(contact);
                return contactDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving contact with ID {ContactId}.", id);
                throw;
            }
        }

        public async Task<ContactDto?> UpdateAsync(Guid id, ContactDto contactDto)
        {
            ContactDto? updateContactDto = null;
            try
            {
                _logger.LogInformation("Attempting to update contact with ID {ContactId}.", id);
                if (id == Guid.Empty) throw new ArgumentException("Invalid contact ID.", nameof(id));
                if (contactDto == null) throw new ArgumentNullException(nameof(contactDto));

                Contact contact = _mapper.Map<Contact>(contactDto);
                Contact? updatedContact = await _contactRepository.UpdateAsync(id, contact);

                updateContactDto = _mapper.Map<ContactDto>(updatedContact);
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
