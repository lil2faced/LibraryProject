using LibraryProject.Applications;
using LibraryProject.Entities.EntityBookLoan;
using LibraryProject.Entities.Orders;
using LibraryProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoanController : ControllerBase
    {
        private readonly BookLoanService bookService;
        public BookLoanController(BookLoanService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookLoan>>> Get()
        {
            return Ok(await bookService.Get());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookLoan>> Get(int id)
        {
            var res = await bookService.GetById(id);
            if (res.Item1 == 0)
                return Ok(res.Item2);
            else
                return NotFound("Запись не найдена");
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BookLoanWithoutExternal bookLoanWithoutExternal, int BookId, int UserId, int StatusId)
        {
            switch (await bookService.Add(bookLoanWithoutExternal, UserId, BookId))
            {
                case 0: return Ok("Запись создана");
                case 1: return NotFound("Запись не найдена");
                default: return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            switch (await bookService.Delete(id))
            {
                case 0: return Ok("Запись удалена");
                case 1: return NotFound("Запись не найдена");
                default: return BadRequest("Неизвестная ошибка");
            }
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, BookLoanWithoutExternal bookLoanWithoutExternal, int statusId, int UserId, int BookId)
        {
            switch (await bookService.Update(bookLoanWithoutExternal,UserId, BookId, id))
            {
                case 0: return Ok("Запись обновлена");
                case 1: return NotFound("Запись не найдена");
                default: return BadRequest("Неизвестная ошибка");
            }
        }
    }
}
