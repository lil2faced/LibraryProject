using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;
using LibraryProject.Entities.EntityBook;
using LibraryProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<List<AuthorDTO>>> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            return Ok(await service.GetAllAuthorsAsync(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAsync(int? id, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            return Ok(await service.GetAuthorByIdAsync(id, cancellationToken));
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] AuthorDTO? author, CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                await service.AddAuthorAsync(author, cancellationToken);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
                await service.DeleteByIdAsync(id);
                return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody]AuthorDTO bookAuthor)
        {
                await service.UpdateByIDAsync(id, bookAuthor);
                return Ok();
        }
    }
}
