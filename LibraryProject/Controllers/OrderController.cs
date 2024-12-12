using LibraryProject.Applications;
using LibraryProject.Entities;
using LibraryProject.Entities.Orders;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        public OrderController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookPurchaseOrder>>> Get()
        {
            return await BookOrderService.Get(_databaseContext);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookPurchaseOrder>> Get(int id)
        {
            var res = await BookOrderService.GetById(_databaseContext, id);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BookPurchaseOrderWithoutExternal bookPurchaseOrderWithoutExternal, int BookId, int UserId, int StatusId)
        {
            switch (await BookOrderService.Add(_databaseContext, bookPurchaseOrderWithoutExternal, StatusId, UserId, BookId))
            {
                case 0: return Ok();
                case 1: return NotFound();
                default: return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            switch (await BookOrderService.Delete(_databaseContext, id))
            {
                case 0: return Ok();
                case 1: return NotFound();
                default: return BadRequest();
            }
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, BookPurchaseOrderWithoutExternal bookPurchaseOrderWithoutExternal, int statusId, int UserId, int BookId)
        {
            switch (await BookOrderService.Update(_databaseContext, bookPurchaseOrderWithoutExternal, statusId, UserId, BookId, id))
            {
                case 0: return Ok();
                case 1: return NotFound();
                default: return BadRequest();
            }
        }
    }
}
