using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<List<LoanDTOChild>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await bookService.Get(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDTOChild>> Get(int? id, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;

                return Ok(await bookService.GetById(id, cancellationToken));
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] LoanDTOParent bookLoanWithoutExternal, int? BookId, int? UserId, int? StatusId, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                await bookService.Add(bookLoanWithoutExternal, UserId, BookId, cancellationToken);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
                await bookService.Delete(id);
                return Ok();

        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int? id, LoanDTOChild bookLoanWithoutExternal, int? statusId, int? UserId, int? BookId)
        {
                await bookService.Update(bookLoanWithoutExternal, UserId, BookId, id);
                return Ok();
        }
    }
}
