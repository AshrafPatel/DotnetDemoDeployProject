using System.ComponentModel.DataAnnotations;

namespace Contacts.Core.Entities
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
