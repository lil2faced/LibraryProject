using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
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
        private readonly CancellationTokenSource _cts;
        public ReviewController(ReviewService reviewService, CancellationTokenSource cts)
        {
            this.reviewService = reviewService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<ReviewDTOChild>>> GetByBookId(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await reviewService.GetAllReviews(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTOChild>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;

                await reviewService.GetReviewById(id, token);
                return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Add(int? bookId, [FromBody]ReviewDTOParent reviewWithoutExternal, CancellationToken token)
        {
            token = _cts.Token;
                await reviewService.AddAsync(bookId, reviewWithoutExternal, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
                await reviewService.DeleteReviewById(id);
                return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(int? id, [FromBody] ReviewDTOParent reviewWithoutExternal)
        {
                await reviewService.UpdateReview(id, reviewWithoutExternal);
                return Ok();
        }
    }
}
