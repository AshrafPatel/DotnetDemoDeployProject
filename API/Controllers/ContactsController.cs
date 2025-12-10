using Contacts.Data.Models;
using Contacts.DTO;
using Contacts.Services.Contacts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Controllers.Contacts
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
            _logger.LogInformation("Getting all contacts");
            List<ContactDto> contacts = await _contactService.GetAllContacts() ?? new List<ContactDto>();
            _logger.LogInformation("Returning {Count} contacts", contacts.Count);
            return Ok(contacts);
        }

        [HttpGet]
        [ActionName("GetAContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAContactAsync([FromRoute] Guid id)
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

        [HttpGet]
        [ActionName("GetAContactsByName")]
        [Route("{name}")]
        public async Task<IActionResult> GetContactsByName([FromRoute] string name)
        {
            _logger.LogInformation("Getting contacts with Name: {ContactName}", name);
            List<ContactDto> contacts = await _contactService.FindContactByName(name) ?? new List<ContactDto>();
            _logger.LogInformation("{Count} contacts found for name {Name}", contacts.Count, name);
            return Ok(contacts);
        }

        [HttpPost]
        [ActionName("AddContactAsync")]
        public async Task<IActionResult> AddContactAsync([FromBody, Required] ContactDto contactDto)
        {
            _logger.LogInformation("Adding a new contact with Name: {ContactName}", contactDto.Name);

            bool isCreated = await _contactService.AddAsync(contactDto);
            if (!isCreated)
            {
                _logger.LogWarning("While logging contact could not be created");
                return BadRequest("Could not create contact.");
            }

            return CreatedAtAction(nameof(GetAContactAsync), new { id = contactDto.Id }, contactDto);
        }

        [HttpPut]
        [ActionName("UpdateContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContactAsync([FromRoute] Guid id, [FromBody, Required] ContactDto contactDto)
        {
            _logger.LogInformation("Updating contact with ID: {ContactId}", id);

            ContactDto contactInDb = await _contactService.UpdateAsync(id, contactDto);   
            if (contactInDb == null)
            {
                _logger.LogWarning("Contact not found: {ContactId}", id);
                return NotFound();
            }
           
            return Ok(contactInDb);
        }

        [HttpDelete]
        [ActionName("DeleteContactAsync")]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] Guid id)
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
    }
}
