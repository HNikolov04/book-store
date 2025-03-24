using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await _bookRepository.GetAllBooksAsync();

        return books.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            ShortDescription = book.ShortDescription,
            FullDescription = book.FullDescription,
            Price = book.Price,
            ImageUrl = book.ImageUrl,
            PurchaseCount = book.PurchaseCount
        }).ToList();
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);

        if (book == null)
        {
            return null;
        }

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            ShortDescription = book.ShortDescription,
            FullDescription = book.FullDescription,
            Price = book.Price,
            ImageUrl = book.ImageUrl,
            PurchaseCount = book.PurchaseCount
        };
    }

    public async Task<BookDto> CreateBookAsync(BookDto bookDto)
    {
        var book = new Book
        {
            Title = bookDto.Title,
            ShortDescription = bookDto.ShortDescription,
            FullDescription = bookDto.FullDescription,
            Price = bookDto.Price,
            ImageUrl = bookDto.ImageUrl,
            PurchaseCount = bookDto.PurchaseCount
        };

        var newBook = await _bookRepository.AddBookAsync(book);

        return new BookDto
        {
            Id = newBook.Id,
            Title = newBook.Title,
            ShortDescription = newBook.ShortDescription,
            FullDescription = newBook.FullDescription,
            Price = newBook.Price,
            ImageUrl = newBook.ImageUrl,
            PurchaseCount = newBook.PurchaseCount
        };
    }

    public async Task<BookDto?> UpdateBookAsync(int id, BookDto bookDto)
    {
        var book = await _bookRepository.GetBookByIdAsync(id);

        if (book == null)
        {
            return null;
        }

        book.Title = bookDto.Title;
        book.ShortDescription = bookDto.ShortDescription;
        book.FullDescription = bookDto.FullDescription;
        book.Price = bookDto.Price;
        book.ImageUrl = bookDto.ImageUrl;
        book.PurchaseCount = bookDto.PurchaseCount;

        var updatedBook = await _bookRepository.UpdateBookAsync(book);

        return new BookDto
        {
            Id = updatedBook.Id,
            Title = updatedBook.Title,
            ShortDescription = updatedBook.ShortDescription,
            FullDescription = updatedBook.FullDescription,
            Price = updatedBook.Price,
            ImageUrl = updatedBook.ImageUrl,
            PurchaseCount = updatedBook.PurchaseCount
        };
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        return await _bookRepository.DeleteBookAsync(id);
    }
}