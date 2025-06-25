using PasswordReminder.Business.Interfaces;
using PasswordReminder.DataAccess;
using PasswordReminder.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace PasswordReminder.Business.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(string email, string password)
        {
            var hash = HashPassword(password);
            var user = new User
            {
                Email = email,
                PasswordHash = hash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var hash = HashPassword(password);
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hash);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        // Basit SHA256 Hash (geliştirilebilir)
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
