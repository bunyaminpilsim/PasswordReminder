using PasswordReminder.Business.Interfaces;
using PasswordReminder.DataAccess;
using PasswordReminder.Entities;
using Microsoft.EntityFrameworkCore;

namespace PasswordReminder.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Category> AddIfNotExistsAsync(string categoryName)
        {
            var existing = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());

            if (existing != null)
                return existing;

            var newCategory = new Category { Name = categoryName };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }
    }
}
