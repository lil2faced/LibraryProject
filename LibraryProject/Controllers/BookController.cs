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
        private readonly DatabaseContext _db;
        public BookController(DatabaseContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAsync()
        {
            return Ok(await BookService.GetAllBooksAsync(_db));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetAsync(int id)
        {
            var res = await BookService.ReturnBookByIdAsync(_db, id);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound("Книга не найдена");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] BookWithoutExternal bookNo, int GenreId, int CategoryId, int AuthorId, int SeriesID)
        {
            switch (await BookService.AddBookAsync(_db, GenreId, CategoryId, AuthorId, SeriesID, bookNo))
            {
                case 0:
                    return Ok("Книга создана");
                case 1:
                    return NotFound("Жанр, Автор, Серия или Категория не найдены");
                case 2:
                    return BadRequest("Книга с таким именем уже существует");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            switch (await BookService.DeleteByIdAsync(_db, id))
            {
                case 0:
                    return Ok("Книга удалена");
                case 1:
                    return NotFound("Книга не найдена");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookWithoutExternal book, int GenreId, int CategoryId, int AuthorId, int SeriesID)
        {
            var result = await BookService.UpdateByIDAsync(_db, id, GenreId, CategoryId, AuthorId, SeriesID, book);
            if (result == 1)
                return NotFound($"Книга с ID {id} не найдена");
            else if (result == 2)
                return NotFound("Один из связанных объектов не найден");
            return Ok("Книга обновлена");
        }
    }
}
