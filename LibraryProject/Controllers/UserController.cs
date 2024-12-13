using LibraryProject.Applications;
using LibraryProject.Entities;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDatabaseSevice userService;
        public UserController(UserDatabaseSevice userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await userService.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetById(int id)
        {
            var res = await userService.Get(id);
            if (res.Item1 == 1)
            {
                return NotFound("Пользователь не найден");
            }
            return Ok(res.Item2);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            int res = await userService.DeleteById(id);
            if (res == 0)
            {
                return Ok("Пользователь удален");
            }
            return NotFound("Пользователь не найден");
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserWithoutExternal user)
        {
            int res = await userService.Add(user);
            if (res == 1)
            {
                return BadRequest("Такой пользователь уже есть");
            }
            return Ok("Пользователь создан");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] User user)
        {
            int res = await userService.Update(id, user);
            if (res == 1)
            {
                return BadRequest("Такого пользователя нету");
            }
            return Ok("Пользователь обновлён");
        }


    }
}
