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
        public async Task<ActionResult<List<CategoryModel>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await categoryService.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                return Ok(await categoryService.GetByIdAsync(id, token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CategoryModel category, CancellationToken token)
        {
            token = _cts.Token;
            
            try
            {
                await categoryService.AddAsync(category, token);
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
                await categoryService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] CategoryModel category)
        {
            try
            {
                await categoryService.UpdateByIDAsync(id, category);
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
