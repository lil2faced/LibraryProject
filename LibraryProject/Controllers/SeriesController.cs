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
        public async Task<ActionResult<List<SeriesDTO>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await seriesService.GetAllAsync(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SeriesDTO>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
                await seriesService.GetByIdAsync(id, token);
                return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] SeriesDTO series, CancellationToken token)
        {
            token = _cts.Token;
                await seriesService.AddAsync(series, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
                await seriesService.DeleteByIdAsync(id);
                return Ok();

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] SeriesDTO series)
        {
                await seriesService.UpdateByIDAsync(id, series);
                return Ok();
        }
    }
}
