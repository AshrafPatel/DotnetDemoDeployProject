using Contacts.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Shared.DTOs
{
    public class ContactDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsEditing { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public State State { get; set; }

    }
}
