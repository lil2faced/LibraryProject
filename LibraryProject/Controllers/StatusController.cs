using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
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
        private readonly StatusService statusService;
        private readonly CancellationTokenSource _cts;
        public StatusController(StatusService statusService, CancellationTokenSource cts)
        {
            this.statusService = statusService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<StatusDTO>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await statusService.GetAll(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDTO>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
                await statusService.GetById(id, token);
                return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]StatusDTO status, CancellationToken token)
        {
            token = _cts.Token;
                await statusService.Add(status, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
                await statusService.Delete(id);
                return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, [FromBody] StatusDTO status)
        {
                await statusService.Update(id, status);
                return Ok();
        }
    }
}
