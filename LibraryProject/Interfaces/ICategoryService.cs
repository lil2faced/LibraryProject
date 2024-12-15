using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryDTO category, CancellationToken cancellationToken);
        Task<List<CategoryDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<CategoryDTO> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, CategoryDTO cat);

    }
}
