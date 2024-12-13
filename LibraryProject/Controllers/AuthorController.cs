using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly DatabaseContext _db;
        public AuthorController(DatabaseContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookAuthor>>> GetAsync()
        {
            return Ok(await AuthorService.GetAllAuthorsAsync(_db));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookAuthor>> GetAsync(int id)
        {
            var res = await AuthorService.GetAuthorByIdAsync(id, _db);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound("Автор не найден");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] BookAuthor author)
        {
            switch (await AuthorService.AddAuthorAsync(author, _db))
            {
                case 0:
                    return Ok("Автор создан");
                case 1:
                    return NotFound("Такой автор уже существует");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            switch (await AuthorService.DeleteByIdAsync(id, _db))
            {
                case 0:
                    return Ok("Автор успешно удалён");
                case 1:
                    return NotFound("Автор не найден");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]BookAuthor bookAuthor)
        {
            var result = await AuthorService.UpdateByIDAsync(_db, id, bookAuthor);
            if (result == 1)
                return NotFound($"Автор с ID {id} не найден");
            return Ok("Автор обновлён");
        }
    }
}
