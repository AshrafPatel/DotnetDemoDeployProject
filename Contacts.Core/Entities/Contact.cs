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
        public string Email { get; set; } = string.Empty;
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
