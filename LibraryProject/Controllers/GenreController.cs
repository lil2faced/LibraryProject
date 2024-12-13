using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public GenreController(DatabaseContext database)
        {
            _databaseContext = database;
        }
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return Ok(await GenreService.GetAllAsync(_databaseContext));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre?>> Get(int id)
        {
            var gen = await GenreService.GetByIdAsync(id, _databaseContext);
            if (gen.Item1 == 0)
                return Ok(gen.Item2);
            else
                return NotFound("Жанр не найден");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Genre genre)
        {
            switch (await GenreService.AddAsync(genre, _databaseContext))
            {
                case 0:
                    return Ok("Жанр создан");
                case 1:
                    return BadRequest("Такой жанр уже существует");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            switch (await GenreService.DeleteByIdAsync(id, _databaseContext))
            {
                case 0:
                    return Ok("Жанр удален");
                case 1:
                    return BadRequest("Ошибка. Жанр не найден");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Genre genre)
        {
            var result = await GenreService.UpdateByIDAsync(_databaseContext, id, genre);
            if (result == 1)
                return NotFound($"Жанр с ID {id} не найден");
            return Ok("Жанр обновлен");
        }
    }
}
