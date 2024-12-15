using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService service;
        private readonly CancellationTokenSource _cts;
        public AuthorController(AuthorService authorService, CancellationTokenSource cts)
        {
            service = authorService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuthorModel>>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await service.GetAllAuthorsAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorModel>> GetAsync(int? id, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await service.GetAuthorByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] AuthorModel? author, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                await service.AddAuthorAsync(author, cancellationToken);
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
                await service.DeleteByIdAsync(id);
                return Ok();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody]AuthorModel bookAuthor)
        {
            try
            {
                await service.UpdateByIDAsync(id, bookAuthor);
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
