using AuthX.Application.Interfaces;
using AuthX.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IService<Book> _bookService;

        public BooksController(IService<Book> bookService)
        {
            _bookService = bookService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bookService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound($"Book with ID {id} not found");

            return Ok(book);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBook = await _bookService.CreateAsync(book);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdBook.Id },
                createdBook
            );
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != book.Id)
                return BadRequest("Book ID mismatch");

            var existingBook = await _bookService.GetByIdAsync(id);
            if (existingBook == null)
                return NotFound($"Book with ID {id} not found");

            var updatedBook = await _bookService.UpdateAsync(book);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Book with ID {id} not found");

            return NoContent();
        }
    }
}
