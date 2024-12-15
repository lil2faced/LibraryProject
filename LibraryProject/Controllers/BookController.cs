using AutoMapper;
using LibraryProject.Applications;

using LibraryProject.Entities.EntityBook;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<List<Book>>> GetAsync(CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                return Ok(await bookService.GetAllBooksAsync(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetAsync(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                return Ok(await bookService.ReturnBookByIdAsync(id, token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] BookWithoutExternal bookNo, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesID, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await bookService.AddBookAsync(GenreId, CategoryId, AuthorId, SeriesID, bookNo, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                await bookService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] BookWithoutExternal book, int? GenreId, int? CategoryId, int? AuthorId, int? SeriesID)
        {
            try
            {
                await bookService.UpdateByIDAsync(id, GenreId, CategoryId, AuthorId, SeriesID, book);
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
