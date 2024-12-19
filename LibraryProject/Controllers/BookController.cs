using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly BookService bookService;
        private readonly CancellationTokenSource _cts;
        public BookController(BookService bookService, CancellationTokenSource cts)
        {
            this.bookService = bookService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookDTOChild>>> GetAsync(CancellationToken token)
        {
            token = _cts.Token;
                return Ok(await bookService.GetAllBooksAsync(token));
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTOChild>> GetAsync(int? id, CancellationToken token)
        {
            token = _cts.Token;
                return Ok(await bookService.ReturnBookByIdAsync(id, token));
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] BookDTOParent bookNo, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesID, CancellationToken token)
        {
            token = _cts.Token;
                await bookService.AddBookAsync(GenreId, CategoryId, AuthorId, SeriesID, bookNo, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
                await bookService.DeleteByIdAsync(id);
                return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] BookDTOParent book, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesID)
        {
                await bookService.UpdateByIDAsync(id, GenreId, CategoryId, AuthorId, SeriesID, book);
                return Ok();
        }
    }
}
