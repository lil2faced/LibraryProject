using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface ICategoryService
    {
        Task<int> AddAsync(Category category);
        Task<List<Category>> GetAllAsync();
        Task<(int, Category?)> GetByIdAsync(int id);
        Task<int> DeleteByIdAsync(int id);
        Task<int> UpdateByIDAsync(int id, Category cat);

    }
}
