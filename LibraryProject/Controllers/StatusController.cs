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
        private readonly StatusService statusService;
        private readonly CancellationTokenSource _cts;
        public StatusController(StatusService statusService, CancellationTokenSource cts)
        {
            this.statusService = statusService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<Status>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await statusService.GetAll(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await statusService.GetById(id, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]Status status, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await statusService.Add(status, token);
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
                await statusService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, [FromBody] Status status)
        {
            try
            {
                await statusService.Update(id, status);
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
