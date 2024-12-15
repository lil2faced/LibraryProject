using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IGenreService
    {
        Task AddAsync(GenreDTO genre, CancellationToken cancellationToken);
        Task<List<GenreDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<GenreDTO> GetByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, GenreDTO gen);
    }
}
