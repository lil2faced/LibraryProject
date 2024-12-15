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
            try
            {
                return Ok(await reviewService.GetAllReviews(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTOChild>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await reviewService.GetReviewById(id, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Add(int? bookId, [FromBody]ReviewDTOParent reviewWithoutExternal, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await reviewService.AddAsync(bookId, reviewWithoutExternal, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                await reviewService.DeleteReviewById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            
            
        }
        [HttpPut]
        public async Task<ActionResult> Update(int? id, [FromBody] ReviewDTOParent reviewWithoutExternal)
        {
            try
            {
                await reviewService.UpdateReview(id, reviewWithoutExternal);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }


        }

    }
}
