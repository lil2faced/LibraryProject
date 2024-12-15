using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(AuthorDTO? author, CancellationToken cancellationToken);
        Task<List<AuthorDTO>> GetAllAuthorsAsync(CancellationToken cancellationToken);
        Task<AuthorDTO> GetAuthorByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, AuthorDTO aut);
    }
}
