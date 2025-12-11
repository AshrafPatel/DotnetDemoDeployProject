using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Core.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
