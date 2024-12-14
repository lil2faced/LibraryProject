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
        private readonly BookOrderService orderService;
        private readonly CancellationTokenSource _cts;
        public OrderController(BookOrderService orderService, CancellationTokenSource cts)
        {
            this.orderService = orderService;
            _cts = cts;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookPurchaseOrder>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
            try
            {
                return Ok(await orderService.Get(cancellationToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookPurchaseOrder>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                return Ok(await orderService.GetById(id, token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BookPurchaseOrderWithoutExternal bookPurchaseOrderWithoutExternal, int? BookId, int? UserId, int? StatusId, CancellationToken token)
        {
            token = _cts.Token;
            try
            {
                await orderService.Add(bookPurchaseOrderWithoutExternal, StatusId, UserId, BookId, token);
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
                await orderService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, BookPurchaseOrderWithoutExternal bookPurchaseOrderWithoutExternal, int? statusId, int? UserId, int? BookId)
        {
            try
            {
                await orderService.Update(bookPurchaseOrderWithoutExternal, statusId, UserId, BookId, id);
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
