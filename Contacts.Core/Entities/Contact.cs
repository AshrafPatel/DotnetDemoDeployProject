using Contacts.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Core.Entities
{
    public class Contact
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Column("name")]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Column("created_at")]
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("state")]
        [Required]
        public State State { get; set; }

    }
}
