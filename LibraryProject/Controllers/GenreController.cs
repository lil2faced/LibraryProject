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
        public async Task<ActionResult<List<GenreModel>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await genreService.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreModel>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                return await genreService.GetByIdAsync(id, token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] GenreModel genre, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await genreService.AddAsync(genre, token);
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
                await genreService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] GenreModel genre)
        {
            try
            {
                await genreService.UpdateByIDAsync(id, genre);
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
