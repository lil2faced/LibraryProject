using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Entities.EntityRewiev;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DatabaseContext databaseContext;
        public ReviewService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task AddAsync(int? BookId, ReviewWithoutExternal reviewWithout, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (BookId == null || reviewWithout == null)
            {
                throw new ArgumentNullException();
            }
            var book = await databaseContext.Books.FindAsync(BookId);
            if (book == null)
            {
                throw new Exception("Книга не найдена");
            }

            Review review = new Review()
            {
                BookId = (int)BookId,
                BodyReview = reviewWithout.BodyReview,
                DateReview = reviewWithout.DateReview
            };

            databaseContext.Reviews.Add(review);
            await databaseContext.SaveChangesAsync();

            book.Reviews.Add(review);

            await databaseContext.SaveChangesAsync();
        }
        public async Task<List<Review>> GetAllReviews(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await databaseContext.Reviews.ToListAsync();
        }
        public async Task<Review?> GetReviewById(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var review = await databaseContext.Reviews.FindAsync(id);
            return review;
        }
        public async Task DeleteReviewById(int? id)
        {
            if (id == null) throw new ArgumentNullException();
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new Exception("Отзыв не найден");
            }
            databaseContext.Reviews.Remove(review);
            await databaseContext.SaveChangesAsync();
        }
        public async Task UpdateReview(int? id, ReviewWithoutExternal rev)
        {
            if (id == null || rev == null) throw new ArgumentNullException();
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new Exception("Отзыв не найден");
            }
            review.DateReview = rev.DateReview;
            review.BodyReview = rev.BodyReview;
            databaseContext.Reviews.Update(review);
            await databaseContext.SaveChangesAsync();
        }
    }
}
