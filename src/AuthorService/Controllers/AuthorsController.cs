using Microsoft.AspNetCore.Mvc;
using Refit;
using Shared;
using Shared.Models;

namespace AuthorService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : BaseTestController
{
    private readonly IRefitRestClient _client;

    public AuthorsController(
        IServiceProvider serviceProvider,
        IRefitRestClient client) : base(serviceProvider)
    {
        _client = client;
    }

    [HttpGet("list")]
    public IEnumerable<Author> GetAllAuthors()
    {
        return StoreDb.Authors;
    }

    [HttpGet("by/{id}")]
    public Author? GetAuthorById(int id)
    {
        return StoreDb.AuthorById(id);
    }

    [HttpGet("/info/{id}")]
    public async Task<AuthorInfo?> GetAuthorInfoById(int id)
    {
        var author = StoreDb.AuthorById(id);
        if (author is null)
        {
            return null;
        }

        //First Methode
        //var booksService = RestService.For<IRefitRestClient>("http://localhost:5108");
        //var books = await booksService.GetBooksByAuthorId(author.Id);

        //Second Methode
        var books = await _client.GetBooksByAuthorId(author.Id);
        var bookResponse = books
            .Select(b => new BookResponseDto(b.Id, b.Code, b.Name)).ToList();

        return new AuthorInfo(author.Id, author.Code, author.Name, bookResponse);
    }

    [HttpGet("/{authorId}/books")]
    public IEnumerable<Book> GetBooksByAuthorId(int authorId)
    {
        return StoreDb.BooksByAuthorId(authorId);
    }

    [HttpPost]
    public async Task<Book> AddBook([FromBody] BookDto book)
    {
        var newBook = await _client.CreateBook(book);
        return newBook;
    }

    [HttpPut("{id}")]
    public async Task<Book> EditBook(int id, [FromBody] BookDto book)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        ArgumentNullException.ThrowIfNull(nameof(book));
        var newBook = await _client.UpdateBook(id, book);
        return newBook;
    }

    [HttpDelete("{id}")]
    public async Task DeleteBookById(int id)
    {
        await _client.RemoveBook(id);
    }
}