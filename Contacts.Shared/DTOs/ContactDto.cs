using System.ComponentModel.DataAnnotations;

namespace Contacts.Shared.DTOs
{
    public class ContactDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsEditing { get; set; }

    }
}
