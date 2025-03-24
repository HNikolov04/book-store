using BookStore.Application.DTOs;

namespace BookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto> CreateBookAsync(BookDto bookDto);
    Task<BookDto?> UpdateBookAsync(int id, BookDto bookDto);
    Task<bool> DeleteBookAsync(int id);
}