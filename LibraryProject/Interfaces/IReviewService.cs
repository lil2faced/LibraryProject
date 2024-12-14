using LibraryProject.Entities.EntityRewiev;

namespace LibraryProject.Interfaces
{
    public interface IReviewService
    {
        Task AddAsync(int? BookId, ReviewWithoutExternal reviewWithout, CancellationToken cancellationToken);
        Task<List<Review>> GetAllReviews(CancellationToken cancellationToken);
        Task<Review?> GetReviewById(int? id, CancellationToken cancellationToken);
        Task DeleteReviewById(int? id);
        Task UpdateReview(int? id, ReviewWithoutExternal rev);
    }
}
