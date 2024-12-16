using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities.BookProps;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;
        private readonly CancellationTokenSource _cts;
        public CategoryController(CategoryService categoryService, CancellationTokenSource cts)
        {
            this.categoryService = categoryService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await categoryService.GetAllAsync(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
                return Ok(await categoryService.GetByIdAsync(id, token));
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CategoryDTO category, CancellationToken token)
        {
            token = _cts.Token;
                await categoryService.AddAsync(category, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
                await categoryService.DeleteByIdAsync(id);
                return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] CategoryDTO category)
        {
                await categoryService.UpdateByIDAsync(id, category);
                return Ok();
        }

    }
}
