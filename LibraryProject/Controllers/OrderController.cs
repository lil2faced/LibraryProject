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
        public OrderController(BookOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookPurchaseOrder>>> Get()
        {
            return Ok(await orderService.Get());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookPurchaseOrder>> Get(int? id)
        {
            var res = await orderService.GetById(id);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound("Заказ не найден");
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BookPurchaseOrderWithoutExternal bookPurchaseOrderWithoutExternal, int? BookId, int? UserId, int? StatusId)
        {
            switch (await orderService.Add(bookPurchaseOrderWithoutExternal, StatusId, UserId, BookId))
            {
                case 0: return Ok("Заказ добавлен");
                case 1: return NotFound("Заказ не найден");
                default: return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            switch (await orderService.Delete(id))
            {
                case 0: return Ok("Заказ удален");
                case 1: return NotFound("Заказ не найден");
                default: return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, BookPurchaseOrderWithoutExternal bookPurchaseOrderWithoutExternal, int? statusId, int? UserId, int? BookId)
        {
            switch (await orderService.Update(bookPurchaseOrderWithoutExternal, statusId, UserId, BookId, id))
            {
                case 0: return Ok("Заказ обновлен");
                case 1: return NotFound("Заказ не найден");
                default: return BadRequest("Неизвестная ошибка");
            }
        }
    }
}
