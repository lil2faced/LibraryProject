using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Interfaces;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService genreService;
        private readonly CancellationTokenSource _cts;
        public GenreController(GenreService genreService, CancellationTokenSource cts)
        {
            this.genreService = genreService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await genreService.GetAllAsync(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
                return await genreService.GetByIdAsync(id, token);
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] GenreDTO genre, CancellationToken token)
        {
            token = _cts.Token;
                await genreService.AddAsync(genre, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
                await genreService.DeleteByIdAsync(id);
                return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] GenreDTO genre)
        {
                await genreService.UpdateByIDAsync(id, genre);
                return Ok();
        }
    }
}
