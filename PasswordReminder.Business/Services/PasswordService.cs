using PasswordReminder.Business.Interfaces;
using PasswordReminder.DataAccess;
using PasswordReminder.Entities;
using Microsoft.EntityFrameworkCore;

namespace PasswordReminder.Business.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly ApplicationDbContext _context;

        public PasswordService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PasswordRecord>> GetPasswordsByUserEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return new List<PasswordRecord>();

            return await _context.PasswordRecords
                .Include(p => p.Category)
                .Where(p => p.UserId == user.Id)
                .ToListAsync();
        }

        public async Task<PasswordRecord> CreateAsync(PasswordRecord record)
        {
            _context.PasswordRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<PasswordRecord?> GetByIdAsync(int id)
        {
            return await _context.PasswordRecords.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");
            return user;
        }

        public async Task UpdateAsync(PasswordRecord record)
        {
            _context.PasswordRecords.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.PasswordRecords.FindAsync(id);
            if (record != null)
            {
                _context.PasswordRecords.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}
