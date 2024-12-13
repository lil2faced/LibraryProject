using LibraryProject.Entities.EntityBook;

namespace LibraryProject.Interfaces
{
    public interface IBookService
    {
        Task<int> AddBookAsync(int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook);
        Task<List<Book>> GetAllBooksAsync();
        Task<(int, Book?)> ReturnBookByIdAsync(int? id);
        Task<int> DeleteByIdAsync(int? id);
        Task<int> UpdateByIDAsync(int? id, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesId, BookWithoutExternal WithoutExternalBook);
    }
}
