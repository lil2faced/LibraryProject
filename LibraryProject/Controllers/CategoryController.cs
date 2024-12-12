using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public CategoryController(DatabaseContext database)
        {
            _databaseContext = database;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return Ok(await CategoryService.GetAllAsync(_databaseContext));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category?>> Get(int id)
        {
            var res = await CategoryService.GetByIdAsync(id, _databaseContext);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Category category)
        {
            switch (await CategoryService.AddAsync(category, _databaseContext))
            {
                case 0:
                    return Ok("Категория создана");
                case 1:
                    return BadRequest("Такая категория уже существует");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            switch (await CategoryService.DeleteByIdAsync(id, _databaseContext))
            {
                case 0:
                    return Ok("Категория удалена");
                case 1:
                    return BadRequest("Ошибка. Категория не найдена");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            var result = await CategoryService.UpdateByIDAsync(_databaseContext, id, category);
            if (result == 1)
                return NotFound($"Категория с ID {id} не найдена");
            return Ok();
        }

    }
}
