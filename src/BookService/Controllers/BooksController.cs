using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Models;

namespace BookService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IServiceProvider serviceProvider)
    : BaseTestController(serviceProvider)
{
    [HttpGet]
    public IEnumerable<Book> GetAllBooks()
    {
        return StoreDb.Books;
    }

    [HttpGet("{id}")]
    public Book? GetBookById(int id)
    {
        return StoreDb.BookById(id);
    }

    [HttpGet("by-author/{authorId}")]
    public IEnumerable<Book> GetBooksByAuthorId(int authorId)
    {
        return StoreDb.BooksByAuthorId(authorId);
    }
    
    [HttpPost]
    public Book AddBook([FromBody] BookDto book)
    {
        return StoreDb.AddBook(book);
    }
    
    [HttpPut("{id}")]
    public Book EditBook(int id, [FromBody] BookDto book)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        ArgumentNullException.ThrowIfNull(nameof(book));
        return StoreDb.EditBook(id,book);
    }
    
    [HttpDelete("{id}")]
    public void DeleteBookById(int id)
    {
         StoreDb.DeleteBook(id);
    }
}