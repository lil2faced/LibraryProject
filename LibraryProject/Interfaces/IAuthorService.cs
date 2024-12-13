using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(BookAuthor author, CancellationToken cancellationToken);
        Task<List<BookAuthor>> GetAllAuthorsAsync(CancellationToken cancellationToken);
        Task<BookAuthor> GetAuthorByIdAsync(int? id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int? id);
        Task UpdateByIDAsync(int? id, BookAuthor aut);
    }
}
