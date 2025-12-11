using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using Contacts.Core.Entities;
using Contacts.Core.Interfaces;
using AutoMapper;
using Contacts.Shared.DTOs;
using Contacts.Services.Exceptions;

namespace Contacts.Services.ContactsService
{
    //Business Logic cant have same email address
    //Business Logic cant have empty name
    //Business Logic change Contact to ContactDto and vice versa

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
                if (string.IsNullOrWhiteSpace(contactDto.Name))
                {
                    _logger.LogWarning("Contact name cannot be empty.");
                    throw new InvalidContactException("Contact name cannot be empty.");
                }
                else if (string.IsNullOrWhiteSpace(contactDto.Email))
                {
                    _logger.LogWarning("Contact email cannot be empty.");
                    throw new InvalidContactException("Contact email cannot be empty.");
                }
                else if (await _contactRepository.IsEmailExists(contactDto.Email))
                {
                    _logger.LogWarning("A contact with email {ContactEmail} already exists.", contactDto.Email);
                    throw new DuplicateEmailException($"A contact with email {contactDto.Email} already exists.");
                }
                else 
                { 
                    _logger.LogInformation("Contact data validated successfully.");
                    Contact contact = _mapper.Map<Contact>(contactDto);
                    await _contactRepository.AddAsync(contact);
                    _logger.LogInformation("Successfully added a new contact with ID {ContactId}.", contact.Id);
                    return true;
                }
                
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
            List<ContactDto>? contacts;
            try
            {
                _logger.LogInformation("Searching for contacts with name: {ContactName}.", name);
                if (string.IsNullOrWhiteSpace(name)) return [];

                List<Contact> contactToFind = await _contactRepository.FindContactByName(name);
                if (contactToFind == null || contactToFind.Count == 0) return [];
                contacts = _mapper.Map<List<ContactDto>>(contactToFind);
                return contacts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for contacts with name: {ContactName}.", name);
                throw;
            }
        }

        public async Task<List<ContactDto>> GetAllContacts()
        {
            List<ContactDto>? contactDtos;
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
            Contact? contactToGet;
            try
            {
                _logger.LogInformation("Retrieving contact with ID {ContactId}.", id);
                if (id == Guid.Empty) throw new ArgumentException("Invalid contact ID.", nameof(id));

                contactToGet = await _contactRepository.GetContactAsync(id);
                if (contactToGet == null) throw new KeyNotFoundException($"Contact with id {id} not found.");
                ContactDto contactDto = _mapper.Map<ContactDto>(contactToGet);
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
            Contact? updateContactDto;
            Contact originalContact = await _contactRepository.GetContactAsync(id);
            try
            {
                _logger.LogInformation("Attempting to update contact with ID {ContactId}.", id);
                if (id == Guid.Empty) throw new ArgumentException("Invalid contact ID.", nameof(id));
                ArgumentNullException.ThrowIfNull(contactDto);

                if (string.IsNullOrWhiteSpace(contactDto.Name))
                {
                    _logger.LogWarning("Contact name cannot be empty.");
                    throw new InvalidContactException("Contact name cannot be empty.");
                }
                else if (string.IsNullOrWhiteSpace(contactDto.Email))
                {
                    _logger.LogWarning("Contact email cannot be empty.");
                    throw new InvalidContactException("Contact email cannot be empty.");
                }
                else if (await _contactRepository.IsEmailExists(contactDto.Email) && originalContact.Email != contactDto.Email)
                {
                    _logger.LogWarning("A contact with email {ContactEmail} already exists.", contactDto.Email);
                    throw new DuplicateEmailException($"A contact with email {contactDto.Email} already exists.");
                }
                else 
                { 
                    _logger.LogInformation("Contact data validated successfully.");
                    Contact contactToUpdate = _mapper.Map<Contact>(contactDto);
                    updateContactDto = await _contactRepository.UpdateAsync(id, contactToUpdate);
                }
                return contactDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating contact with ID {ContactId}.", id);
                throw;
            }
        }
    }
}
