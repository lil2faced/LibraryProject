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
        private readonly DatabaseContext databaseContext;
        public ReviewController(DatabaseContext database)
        {
            databaseContext = database;
        }
        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetByBookId()
        {
            return Ok(await ReviewService.GetAllReviews(databaseContext));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> Get(int id)
        {
            var res = await ReviewService.GetReviewById(databaseContext, id);
            if (res == null)
            {
                return BadRequest("Такого отзыва нету");
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Add(int bookId, [FromBody]ReviewWithoutExternal reviewWithoutExternal)
        {
            int res = await ReviewService.AddAsync(databaseContext, bookId, reviewWithoutExternal);
            if (res == 1)
            {
                return BadRequest("Книга с таким Id не найдена");
            }
            return Ok("Создание успешно");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var res = await ReviewService.DeleteReviewById(databaseContext, id);
            if (res == 1)
            {
                return NotFound("Отзыв не найден");
            }
            return Ok("Отзыв удалён");
        }
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] ReviewWithoutExternal reviewWithoutExternal)
        {
            var res = await ReviewService.UpdateReview(databaseContext, id, reviewWithoutExternal);
            if (res == 1)
            {
                return NotFound("Отзыв не найден");
            }
            return Ok("Отзыв обновлён");
        }

    }
}
