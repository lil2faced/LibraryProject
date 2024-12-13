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
        public async Task<int> AddAsync(int? BookId, ReviewWithoutExternal reviewWithout)
        {
            if (BookId == null || reviewWithout == null)
            {
                throw new ArgumentNullException();
            }
            var book = await databaseContext.Books.FindAsync(BookId);
            if (book == null)
            {
                return 1;
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

            return 0;
        }
        public async Task<List<Review>> GetAllReviews()
        {
            return await databaseContext.Reviews.ToListAsync();
        }
        public async Task<Review?> GetReviewById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var review = await databaseContext.Reviews.FindAsync(id);
            return review;
        }
        public async Task<int> DeleteReviewById(int? id)
        {
            if (id == null) throw new ArgumentNullException();
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return 1;
            }
            databaseContext.Reviews.Remove(review);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public async Task<int> UpdateReview(int? id, ReviewWithoutExternal rev)
        {
            if (id == null || rev == null) throw new ArgumentNullException();
            var review = await databaseContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return 1;
            }
            review.DateReview = rev.DateReview;
            review.BodyReview = rev.BodyReview;
            databaseContext.Reviews.Update(review);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
    }
}
