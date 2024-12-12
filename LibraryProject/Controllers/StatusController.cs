using LibraryProject.Applications;
using LibraryProject.Entities.Orders;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public StatusController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Status>>> Get()
        {
            return await StatusService.GetAll(_databaseContext);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> Get(int id)
        {
            var res = await StatusService.GetById(_databaseContext, id);
            if (res.Item1 == 1)
            {
                return NotFound("Статус не найден");
            }
            return Ok(res.Item2);
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]Status status)
        {
            int res = await StatusService.Add(_databaseContext, status);
            if (res == 1)
            {
                return BadRequest("Такой статус уже есть");
            }
            return Ok("Статус создан");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            int res = await StatusService.Delete(_databaseContext, id);
            if (res == 0)
            {
                return Ok("Статус удален");
            }
            return NotFound("Статус не найден");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Status status)
        {
            int res = await StatusService.Update(_databaseContext, id, status);
            if (res == 1)
            {
                return BadRequest("Такого статуса нету");
            }
            return Ok("Статус обновлён");
        }
    }
}
