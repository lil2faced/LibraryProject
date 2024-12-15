using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;
using AutoMapper;
using LibraryProject.ControllerModels;

namespace LibraryProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;
        public CategoryService(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            _db = databaseContext;
        }
        public async Task AddAsync(CategoryDTO category, CancellationToken cancellationToken)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            var res = _mapper.Map<Category>(category);
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            var category1 = await _db.Categories.Where(a => a.Name == category.Name).FirstOrDefaultAsync();
            if (category1 != null)
            {
                throw new Exception("Такая категория уже существует");
            }
            _db.Categories.Add(res);
            await _db.SaveChangesAsync();
        }
        public async Task<List<CategoryDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.Categories
                .Select(a => _mapper.Map<CategoryDTO>(a))
                .ToListAsync();
        }
        public async Task<CategoryDTO> GetByIdAsync(int? id, CancellationToken cancellationToken)
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
            return _mapper.Map<CategoryDTO>(category);
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
        public async Task UpdateByIDAsync(int? id, CategoryDTO cat)
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
