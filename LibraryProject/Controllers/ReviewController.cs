using LibraryProject.Applications;
using LibraryProject.Entities.EntityRewiev;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService reviewService;
        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetByBookId()
        {
            return Ok(await reviewService.GetAllReviews());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> Get(int id)
        {
            var res = await reviewService.GetReviewById(id);
            if (res == null)
            {
                return BadRequest("Такого отзыва нету");
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Add(int bookId, [FromBody]ReviewWithoutExternal reviewWithoutExternal)
        {
            int res = await reviewService.AddAsync(bookId, reviewWithoutExternal);
            if (res == 1)
            {
                return BadRequest("Книга с таким Id не найдена");
            }
            return Ok("Создание успешно");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await reviewService.DeleteReviewById(id);
            if (res == 1)
            {
                return NotFound("Отзыв не найден");
            }
            return Ok("Отзыв удалён");
        }
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] ReviewWithoutExternal reviewWithoutExternal)
        {
            var res = await reviewService.UpdateReview(id, reviewWithoutExternal);
            if (res == 1)
            {
                return NotFound("Отзыв не найден");
            }
            return Ok("Отзыв обновлён");
        }

    }
}
