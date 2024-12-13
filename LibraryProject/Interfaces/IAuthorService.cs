using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;

namespace LibraryProject.Interfaces
{
    public interface IAuthorService
    {
        Task<int> AddAuthorAsync(BookAuthor author);
        Task<List<BookAuthor>> GetAllAuthorsAsync();
        Task<(int, BookAuthor?)> GetAuthorByIdAsync(int id);
        Task<int> DeleteByIdAsync(int id);
        Task<int> UpdateByIDAsync(int id, BookAuthor aut);
    }
}
