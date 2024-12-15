using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(AuthorModel? author, CancellationToken cancellationToken);
        Task<List<AuthorModel>> GetAllAuthorsAsync(CancellationToken cancellationToken);
        Task<AuthorModel> GetAuthorByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, AuthorModel aut);
    }
}
