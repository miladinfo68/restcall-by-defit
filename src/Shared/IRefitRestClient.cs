using Refit;
using Shared.Models;

namespace Shared;

public interface IRefitRestClient
{
    [Get("/api/books")]
    Task<IEnumerable<Book>> GetAllBooks();

    [Get("/api/books/{id}")]
    Task<Book?> GetBookById(int id);

    [Get("/api/books/by-author/{authorId}")]
    Task<IEnumerable<Book>> GetBooksByAuthorId(int authorId);

    [Post("/api/books")]
    Task<Book> CreateBook([Body] BookDto book);

    [Put("/api/books/{id}")]
    Task<Book> UpdateBook(int id, [Body] BookDto book);
       
    [Delete("/api/books/{id}")]
    Task RemoveBook(int id);
   
}