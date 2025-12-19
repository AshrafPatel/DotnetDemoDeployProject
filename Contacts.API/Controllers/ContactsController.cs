using AutoMapper;
using Contacts.Services.ContactsService;
using Contacts.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Contacts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ActionName("GetAllContactsAsync")]
        public async Task<IActionResult> GetAllContactsAsync()
        {
            try
            {
                _logger.LogInformation("Getting all contacts");
                List<ContactDto> contacts = await _contactService.GetAllContacts() ?? [];
                _logger.LogInformation("Returning {Count} contacts", contacts.Count);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all contacts");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [ActionName("GetAContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAContactAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("Getting contact with ID: {ContactId}", id);
                ContactDto? contact = await _contactService.GetContactAsync(id);
                if (contact == null)
                {
                    _logger.LogWarning("No contact found with ID: {ContactId}", id);
                    return NotFound();
                }
                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting contact with ID: {ContactId}", id);
                return NotFound();
            }
        }

        [HttpGet]
        [ActionName("GetAContactsByName")]
        [Route("{name}")]
        public async Task<IActionResult> GetContactsByName([FromRoute] string name)
        {
            try
            {
                _logger.LogInformation("Getting contacts with Name: {ContactName}", name);
                List<ContactDto> contacts = await _contactService.FindContactByName(name) ?? [];
                _logger.LogInformation("{Count} contacts found for name {Name}", contacts.Count, name);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting contacts with Name: {ContactName}", name);
                return NotFound();
            }
        }

        [HttpPost]
        [ActionName("AddContactAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody] ContactDto contactDto)
        {
            try
            {
                _logger.LogInformation("Adding a new contact with Name: {ContactName}", contactDto.Name);

                bool isCreated = await _contactService.AddAsync(contactDto);
                if (!isCreated)
                {
                    _logger.LogWarning("While logging contact could not be created");
                    return BadRequest("Could not create contact.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for contact add: {ContactId}", contactDto);
                    return BadRequest(ModelState);
                }

                return CreatedAtAction(nameof(GetAContactAsync), new { id = contactDto.Id }, contactDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new contact with Name: {ContactName}", contactDto.Name);
                return BadRequest("An error occurred while adding the contact.");
            }
        }

        [HttpPut]
        [ActionName("UpdateContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContactAsync([FromRoute] Guid id, [FromBody] ContactDto contactDto)
        {
            try
            {
                _logger.LogInformation("Updating contact with ID: {ContactId}", id);

                ContactDto? contactInDb = await _contactService.UpdateAsync(id, contactDto);
                if (contactInDb == null)
                {
                    _logger.LogWarning("Contact not found: {ContactId}", id);
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for contact update: {ContactId}", id);
                    return BadRequest(ModelState);
                }

                return Ok(contactInDb);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating contact with ID: {ContactId}", id);
                return BadRequest("An error occurred while updating the contact.");
            }
        }

        [HttpDelete]
        [ActionName("DeleteContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("Deleting contact with ID: {ContactId}", id);

                bool isDeleted = await _contactService.DeleteAsync(id);
                if (isDeleted == false)
                {
                    _logger.LogWarning("Contact not found or could not be deleted: {ContactId}", id);
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting contact with ID: {ContactId}", id);
                return BadRequest("An error occurred while deleting the contact.");
            }
        }
    }
}
