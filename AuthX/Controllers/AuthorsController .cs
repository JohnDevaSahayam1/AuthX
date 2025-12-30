using AuthX.Application.Interfaces;
using AuthX.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IService<Author> _authorService;

        public AuthorsController(IService<Author> authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);

            if (author == null)
                return NotFound($"Author with ID {id} not found");

            return Ok(author);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAuthor = await _authorService.CreateAsync(author);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdAuthor.Id },
                createdAuthor
            );
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != author.Id)
                return BadRequest("Author ID mismatch");

            var existingAuthor = await _authorService.GetByIdAsync(id);
            if (existingAuthor == null)
                return NotFound($"Author with ID {id} not found");

            var updatedAuthor = await _authorService.UpdateAsync(author);
            return Ok(updatedAuthor);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _authorService.DeleteAsync(id);

            if (!deleted)
                return NotFound($"Author with ID {id} not found");

            return NoContent();
        }
    }
}
