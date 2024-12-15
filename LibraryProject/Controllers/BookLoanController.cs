using AutoMapper;
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
        private readonly BookLoanService bookService;
        private readonly CancellationTokenSource _cts;
        public BookLoanController(BookLoanService bookService, CancellationTokenSource cts)
        {
            this.bookService = bookService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookLoan>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await bookService.Get(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookLoan>> Get(int? id, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await bookService.GetById(id, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BookLoanWithoutExternal bookLoanWithoutExternal, int? BookId, int? UserId, int? StatusId, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                await bookService.Add(bookLoanWithoutExternal, UserId, BookId, cancellationToken);
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
                await bookService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
                throw;
            }

        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int? id, BookLoanWithoutExternal bookLoanWithoutExternal, int? statusId, int? UserId, int? BookId)
        {
            try
            {
                await bookService.Update(bookLoanWithoutExternal, UserId, BookId, id);
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
