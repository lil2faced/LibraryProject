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
        private readonly CancellationTokenSource _cts;
        public UserController(UserDatabaseSevice userService, CancellationTokenSource cts)
        {
            this.userService = userService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await userService.GetAll(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetById(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await userService.Get(id, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                await userService.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserWithoutExternal user, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await userService.Add(user, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, [FromBody] User user)
        {
            try
            {
                await userService.Update(id, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }


    }
}
