using LibraryApi.Data;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound("Book not found with this ID.");

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            if (book == null)
                return BadRequest("Invalid book data.");

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            if (updatedBook == null)
                return BadRequest("Invalid book data.");

            if (id != updatedBook.Id)
                return BadRequest("ID in URL does not match ID in body.");

            var exists = await _context.Books.AnyAsync(b => b.Id == id);
            if (!exists)
                return NotFound("Book not found for update.");

            _context.Entry(updatedBook).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Book updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound("Book not found. Cannot delete.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Book deleted successfully.");
        }
    }
}
