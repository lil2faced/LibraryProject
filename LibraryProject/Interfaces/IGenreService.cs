using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IGenreService
    {
        Task AddAsync(GenreModel genre, CancellationToken cancellationToken);
        Task<List<GenreModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<GenreModel> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, GenreModel gen);
    }
}
