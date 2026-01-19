using Contacts.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Core.Entities
{
    public class Contact
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public State State { get; private set; }

    }
}
