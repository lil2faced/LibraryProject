using LibraryProject.Applications;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoanController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public BookLoanController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookLoan>>> Get()
        {
            return await BookLoanService.Get(_databaseContext);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookLoan>> Get(int id)
        {
            var res = await BookLoanService.GetById(_databaseContext, id);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BookLoanWithoutExternal bookLoanWithoutExternal, int BookId, int UserId, int StatusId)
        {
            switch (await BookLoanService.Add(_databaseContext, bookLoanWithoutExternal, UserId, BookId))
            {
                case 0: return Ok();
                case 1: return NotFound();
                default: return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            switch (await BookLoanService.Delete(_databaseContext, id))
            {
                case 0: return Ok();
                case 1: return NotFound();
                default: return BadRequest();
            }
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, BookLoanWithoutExternal bookLoanWithoutExternal, int statusId, int UserId, int BookId)
        {
            switch (await BookLoanService.Update(_databaseContext, bookLoanWithoutExternal,UserId, BookId, id))
            {
                case 0: return Ok();
                case 1: return NotFound();
                default: return BadRequest();
            }
        }
    }
}
