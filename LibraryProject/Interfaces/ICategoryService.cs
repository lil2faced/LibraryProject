using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryModel category, CancellationToken cancellationToken);
        Task<List<CategoryModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<CategoryModel> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, CategoryModel cat);

    }
}
