using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Interfaces;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService genreService;
        public GenreController(GenreService genreService)
        {
            this.genreService = genreService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            return Ok(await genreService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre?>> Get(int id)
        {
            var gen = await genreService.GetByIdAsync(id);
            if (gen.Item1 == 0)
                return Ok(gen.Item2);
            else
                return NotFound("Жанр не найден");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Genre genre)
        {
            switch (await genreService.AddAsync(genre))
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
            switch (await genreService.DeleteByIdAsync(id))
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
            var result = await genreService.UpdateByIDAsync(id, genre);
            if (result == 1)
                return NotFound($"Жанр с ID {id} не найден");
            return Ok("Жанр обновлен");
        }
    }
}
