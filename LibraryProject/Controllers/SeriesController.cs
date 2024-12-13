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
        private readonly SeriesService seriesService;
        public SeriesController(SeriesService seriesService)
        {
            this.seriesService = seriesService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Series>>> Get()
        {
            return Ok(await seriesService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Series?>> Get(int id)
        {
            var ser = await seriesService.GetByIdAsync(id);
            if (ser.Item1 == 0)
                return Ok(ser.Item2);
            else
                return NotFound("Серия не найдена");
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Series series)
        {
            switch (await seriesService.AddAsync(series))
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
            switch (await seriesService.DeleteByIdAsync(id))
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
            var result = await seriesService.UpdateByIDAsync(id, series);
            if (result == 1)
                return NotFound($"Серия с ID {id} не найдена");
            return Ok("Серия обновлена");
        }
    }
}
