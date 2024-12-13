using LibraryProject.Entities.EntityRewiev;

namespace LibraryProject.Interfaces
{
    public interface IReviewService
    {
        Task<int> AddAsync(int BookId, ReviewWithoutExternal reviewWithout);
        Task<List<Review>> GetAllReviews();
        Task<Review?> GetReviewById(int id);
        Task<int> DeleteReviewById(int id);
        Task<int> UpdateReview(int id, ReviewWithoutExternal rev);
    }
}
