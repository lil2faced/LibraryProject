using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
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
        public async Task<ActionResult<List<UserDTOChild>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await userService.GetAll(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTOChild>> GetById(int? id, CancellationToken token)
        {
            token = _cts.Token;
                await userService.Get(id, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
                await userService.DeleteById(id);
                return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserDTOParent user, CancellationToken token)
        {
            token = _cts.Token;
                await userService.Add(user, token);
                return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, [FromBody] UserDTOChild user)
        {
                await userService.Update(id, user);
                return Ok();
        }


    }
}
