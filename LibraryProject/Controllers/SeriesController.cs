using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
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
        private readonly CancellationTokenSource _cts;
        public SeriesController(SeriesService seriesService, CancellationTokenSource cts)
        {
            this.seriesService = seriesService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<SeriesModel>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await seriesService.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SeriesModel>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await seriesService.GetByIdAsync(id, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] SeriesModel series, CancellationToken token)
        {
            token = _cts.Token;
            
            try
            {
                await seriesService.AddAsync(series, token);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            
            try
            {
                await seriesService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] SeriesModel series)
        {
            try
            {
                await seriesService.UpdateByIDAsync(id, series);
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
