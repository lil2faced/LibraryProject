using LibraryProject.ControllerModels;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly UserApiService _userApiService;
        public UserApiController(UserApiService userApiService)
        {
            _userApiService = userApiService;
        }
        [HttpPost]
        [Route("/Login")]
        public async Task<ActionResult<string>> Login([FromBody] UserApiDTO userApi, CancellationToken cancellationToken)
        {
            var jwt = await _userApiService.Login(userApi, cancellationToken);
            Response.Cookies.Append("auth-token", jwt);
            return Ok();
        }
        [HttpPost]
        [Route("/Register")]
        public async Task<ActionResult> Register([FromBody] UserApiDTO userApi, CancellationToken cancellationToken)
        {
            await _userApiService.Register(userApi, cancellationToken);

            return Ok();
        }
    }
}
