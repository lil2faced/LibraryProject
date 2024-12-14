using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(Category category, CancellationToken cancellationToken);
        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
        Task<Category> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, Category cat);

    }
}
