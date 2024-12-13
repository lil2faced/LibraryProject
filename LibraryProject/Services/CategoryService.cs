using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _db;
        public CategoryService(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }
        public async Task<int> AddAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            var category1 = await _db.Categories.Where(a => a.Name == category.Name).FirstOrDefaultAsync();
            if (category1 != null)
            {
                return 1;
            }
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return 0;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<(int, Category?)> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return (1, category);
            }
            return (0, category);
        }
        public async Task<int> DeleteByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return 1;
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return 0;
        }
        public async Task<int> UpdateByIDAsync(int? id, Category cat)
        {
            if (id == null || cat == null)
            {
                throw new ArgumentNullException();
            }
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return 1;
            }
            category.Name = cat.Name;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return 0;
        }
    }
}
