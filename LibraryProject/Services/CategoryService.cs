using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class CategoryService
    {
        public static async Task<int> AddAsync(Category category, DatabaseContext _db)
        {
            var category1 = await _db.Categories.Where(a => a.Name == category.Name).FirstOrDefaultAsync();
            if (category1 != null)
            {
                return 1;
            }
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return 0;
        }
        public static async Task<List<Category>> GetAllAsync(DatabaseContext _db)
        {
            return await _db.Categories.ToListAsync();
        }
        public static async Task<(int, Category?)> GetByIdAsync(int id, DatabaseContext _db)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return (1, category);
            }
            return (0, category);
        }
        public static async Task<int> DeleteByIdAsync(int id, DatabaseContext _db)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                return 1;
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return 0;
        }
        public static async Task<int> UpdateByIDAsync(DatabaseContext _db, int id, Category cat)
        {
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
