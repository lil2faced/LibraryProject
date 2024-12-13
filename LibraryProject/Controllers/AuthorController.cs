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

        private readonly AuthorService service;
        public AuthorController(AuthorService authorService)
        {
            service = authorService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookAuthor>>> GetAsync()
        {
            return Ok(await service.GetAllAuthorsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookAuthor>> GetAsync(int? id)
        {
            var res = await service.GetAuthorByIdAsync(id);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound("Автор не найден");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] BookAuthor? author)
        {
            switch (await service.AddAuthorAsync(author))
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
        public async Task<IActionResult> Delete(int? id)
        {
            switch (await service.DeleteByIdAsync(id))
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
        public async Task<IActionResult> Update(int? id, [FromBody]BookAuthor bookAuthor)
        {
            var result = await service.UpdateByIDAsync(id, bookAuthor);
            if (result == 1)
                return NotFound($"Автор с ID {id} не найден");
            return Ok("Автор обновлён");
        }
    }
}
