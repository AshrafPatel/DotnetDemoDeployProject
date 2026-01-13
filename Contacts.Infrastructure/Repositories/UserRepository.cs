using Contacts.Core.Entities;
using Contacts.Core.Interfaces;
using Contacts.Infrastructure.Data;
using Contacts.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContactDbContext _contactDbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ContactDbContext contactDbContext, ILogger<UserRepository> logger)
        {
            _contactDbContext = contactDbContext ?? throw new ArgumentNullException(nameof(contactDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }
                user.Id = Guid.NewGuid();
                await _contactDbContext.AddAsync(user);
                await _contactDbContext.SaveChangesAsync();

            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Failed to add user: {Message}", ex.Message);
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new PersistenceException("Failed to add user", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a user: {Message}", ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            User? user;
            if (id == Guid.Empty)
            {
                throw new ArgumentException($"Invalid user ID {id}");
            }
            try
            {
                user = await _contactDbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
                if (user == null) { throw new ArgumentNullException(); }
                _contactDbContext.Users.Remove(user);
                await _contactDbContext.SaveChangesAsync();
                _logger.LogInformation("Deleted user with ID {ContactId}", id);

            }
            catch (DbUpdateException ex)
            {
                throw new PersistenceException("Failed to delete user", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to find user with ID {ContactId}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(email);
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be null or empty", nameof(email));
                }
                return await _contactDbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Email cannot be null: {Message}", ex.Message);
                throw;
            }
            catch(DbUpdateException ex)
            {
                throw new PersistenceException("Failed to retrieve user by email", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving user by email: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new ArgumentException($"Invalid user ID {id}");
                }
                return await _contactDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
            }
            catch (DbUpdateException ex)
            {
                throw new PersistenceException("Failed to retrieve user by email", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving user by email: {Message}", ex.Message);
                throw;
            }
        }
    }
}
