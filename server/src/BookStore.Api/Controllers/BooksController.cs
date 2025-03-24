using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
    {
        var newBook = await _bookService.CreateBookAsync(bookDto);

        return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
    {
        var updatedBook = await _bookService.UpdateBookAsync(id, bookDto);

        if (updatedBook == null)
        {
            return NotFound();
        }

        return Ok(updatedBook);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}