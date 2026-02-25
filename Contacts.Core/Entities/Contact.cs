using Contacts.Shared.Enums;
using Contacts.Shared.Exceptions;
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
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        private Contact(Guid id, string email, string name, State state, DateTime createdAt)
        {
            Id = id;
            Email = email;
            Name = name;
            State = state;
            CreatedAt = createdAt;
        }

        public static Contact Create(Guid id, string email, string name, State state, DateTime createdAt)
        {
            ValidateEmail(email);
            ValidateName(name);
            return new Contact(id, email, name, state, createdAt);
        }

        public void Update(string email, string name, State state, DateTime updatedAt)
        {
            ValidateEmail(email);
            ValidateName(name);

            Email = email;
            Name = name;
            State = state;
            UpdatedAt = updatedAt;
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required");

            if (name.Length < 2)
                throw new DomainException("Name must be at least 2 characters");

            if (name.Length > 100)
                throw new DomainException("Name cannot exceed 100 characters");
        }

        private static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email is required");

            if (!email.Contains("@"))
                throw new DomainException("Invalid email format");

            if (email.Length > 255)
                throw new DomainException("Email cannot exceed 255 characters");
        }
    }
}
