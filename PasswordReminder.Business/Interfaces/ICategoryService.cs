using PasswordReminder.Entities;

namespace PasswordReminder.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> AddIfNotExistsAsync(string categoryName);
    }
}
