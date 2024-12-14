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
        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            var category1 = await _db.Categories.Where(a => a.Name == category.Name).FirstOrDefaultAsync();
            if (category1 != null)
            {
                throw new Exception("Такая категория уже существует");
            }
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.Categories.ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Категория не найдена");
            }
            return category;
        }
        public async Task DeleteByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Категория не найдена");
            }
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateByIDAsync(int? id, Category cat)
        {
            if (id == null || cat == null)
            {
                throw new ArgumentNullException();
            }
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Категория не найдена");
            }
            category.Name = cat.Name;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }
    }
}
