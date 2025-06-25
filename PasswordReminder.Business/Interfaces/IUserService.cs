using PasswordReminder.Entities;

namespace PasswordReminder.Business.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string email, string password);
        Task<User?> LoginAsync(string email, string password);
        Task<bool> EmailExistsAsync(string email);  // Değişti
    }

}
