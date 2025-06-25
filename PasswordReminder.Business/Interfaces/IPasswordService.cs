using PasswordReminder.Entities;

namespace PasswordReminder.Business.Interfaces
{
    public interface IPasswordService
    {
        Task<List<PasswordRecord>> GetPasswordsByUserEmailAsync(string email);
        Task<PasswordRecord> CreateAsync(PasswordRecord record);
        Task<PasswordRecord?> GetByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);

        Task UpdateAsync(PasswordRecord record);
        Task DeleteAsync(int id);
    }
}
