using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities;
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
        public async Task<ActionResult<List<OrderDTOChild>>> Get(CancellationToken cancellationToken)
        {
            cancellationToken = _cts.Token;
                return Ok(await orderService.Get(cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTOChild>> Get(int? id, CancellationToken token)
        {
            token = _cts.Token;
                return Ok(await orderService.GetById(id, token));
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] OrderDTOParent bookPurchaseOrderWithoutExternal, int? BookId, int? UserId, int? StatusId, CancellationToken token)
        {
            token = _cts.Token;
                await orderService.Add(bookPurchaseOrderWithoutExternal, StatusId, UserId, BookId, token);
                return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
                await orderService.Delete(id);
                return Ok();
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, OrderDTOParent bookPurchaseOrderWithoutExternal, int? statusId, int? UserId, int? BookId)
        {
                await orderService.Update(bookPurchaseOrderWithoutExternal, statusId, UserId, BookId, id);
                return Ok();
        }
    }
}
