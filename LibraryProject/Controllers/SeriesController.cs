using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public SeriesController(DatabaseContext database)
        {
            _databaseContext = database;
        }
        [HttpGet]
        public async Task<ActionResult<List<Series>>> Get()
        {
            return Ok(await SeriesService.GetAllAsync(_databaseContext));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Series?>> Get(int id)
        {
            var ser = await SeriesService.GetByIdAsync(id, _databaseContext);
            if (ser.Item1 == 0)
                return Ok(ser.Item2);
            else
                return NotFound("Серия не найдена");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Series series)
        {
            switch (await SeriesService.AddAsync(series, _databaseContext))
            {
                case 0:
                    return Ok("Серия книг создана");
                case 1:
                    return BadRequest("Такая серия книг уже существует");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            switch (await SeriesService.DeleteByIdAsync(id, _databaseContext))
            {
                case 0:
                    return Ok("Серия удалена");
                case 1:
                    return BadRequest("Ошибка. Серия не найдена");
                default:
                    return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Series series)
        {
            var result = await SeriesService.UpdateByIDAsync(_databaseContext, id, series);
            if (result == 1)
                return NotFound($"Серия с ID {id} не найдена");
            return Ok("Серия обновлена");
        }
    }
}
