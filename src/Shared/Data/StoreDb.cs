using Shared.Models;

namespace Shared.Data;

public record StoreDb
{
    private readonly HashSet<Book> _books = [];
    private readonly HashSet<Author> _authors = [];


    private StoreDb()
    {
        _books =
        [
            new Book(1, "A001", "C# 12", 1),
            new Book(2, "A002", "C++ for industry", 1),
            new Book(3, "A003", "You don't know js", 1),

            new Book(4, "B001", "DDD", 2),
            new Book(5, "B002", "Clean architecture", 2),
            
            new Book(6, "B003", "Easy English", 3),
        ];
        _authors =
        [
            new Author(1, "111-2-3", "John Doe"),
            new Author(1, "222-2-3", "Mahdi Jalali"),
        ];
    }

    public static StoreDb Create() => new StoreDb();

    // ################################## BOOK ##################################
    // ################################## BOOK ##################################
    // ################################## BOOK ##################################
    private int NewBookId => _books?.MaxBy(x => x.Id)?.Id ?? 0 + 1;

    public IEnumerable<Book> Books => _books;

    public Book? BookById(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        return _books.FirstOrDefault(x => x.Id == id);
    }

    public BookInfo? BookInfoById(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        var book = _books.FirstOrDefault(x => x.Id == id);
        if (book is null) return null;
        var authorName = _authors.FirstOrDefault(x => x.Id == book.AuthorId)?.Name ?? "";
        return new BookInfo(book.Id, book.Code, book.Name, book.AuthorId, authorName);
    }

    public Book AddBook(BookDto book)
    {
        //ToDo: validations
        var id = NewBookId;
        _books.Add(new Book(NewBookId, book.Code, book.Name, book.AuthorId));
        return BookById(id) !;
    }

    public Book? EditBook(int id, BookDto book)
    {
        var item = BookById(id);
        if (item is null) return null;
        _books.Remove(item);
        var newBook = new Book(id, book.Code, book.Name, book.AuthorId);
        _books.Add(newBook);
        return newBook;
    }

    public void DeleteBook(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        var item = BookById(id);
        if (item is null) return;
        _books.Remove(item);
    }

    // ################################## AUTHOR ##################################
    // ################################## AUTHOR ##################################
    // ################################## AUTHOR ##################################

    private int NewAuthorId => _authors?.MaxBy(x => x.Id)?.Id ?? 0 + 1;

    public IEnumerable<Author> Authors => _authors;

    public Author? AuthorById(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        return _authors.FirstOrDefault(x => x.Id == id);
    }

    public AuthorInfo? AuthorInfoById(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        var author = _authors.FirstOrDefault(x => x.Id == id);

        if (author is null) return null;

        var books = _books
            .Where(w => w.AuthorId == id)?
            .Select(b => new BookResponseDto(b.Id, b.Code, b.Name))
            .ToList() ?? Enumerable.Empty<BookResponseDto>();

        return new AuthorInfo(author.Id, author.Code, author.Name, books.OrderBy(o => o.Id));
    }

    public IEnumerable<Book> BooksByAuthorId(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        var books = _books
            .Where(w => w.AuthorId == id)?
            .ToList() ?? Enumerable.Empty<Book>();
        return books.OrderBy(o => o.Id);
    }

    public void AddAuthor(AuthorDto author)
    {
        //ToDo: validations
        _authors.Add(new Author(NewAuthorId, author.Code, author.Name));
    }

    public void EditAuthor(int id, AuthorDto author)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        var item = AuthorById(id);
        if (item is null) return;
        _authors.Remove(item);
        _authors.Add(new Author(id, author.Code, author.Name));
    }

    public void DeleteAuthor(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        var item = AuthorById(id);
        if (item is null) return;
        _authors.Remove(item);
    }
}