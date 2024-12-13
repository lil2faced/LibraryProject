using LibraryProject.Entities.EntityBookLoan;

namespace LibraryProject.Interfaces
{
    public interface ILoanService
    {
        Task<int> Add(BookLoanWithoutExternal bookLoanWithoutExternal, int? UserId, int? BookId);
        Task<(int, BookLoan?)> GetById(int? id);
        Task<List<BookLoan>> Get();
        Task<int> Delete(int? id);
        Task<int> Update(BookLoanWithoutExternal bookLoanWithoutExternal, int? UserId, int? BookId, int? id);
    }
}
