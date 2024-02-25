namespace Shared.Models;


public record Book(int Id, string Code, string Name, int AuthorId);
public  record BookDto( string Code, string Name, int AuthorId);

public record BookResponseDto(int Id, string Code, string Name);

public record Author(int Id, string Code, string Name);
public  record AuthorDto( string Code, string Name);

public record BookInfo(int BookId, string BookCode, string BookName, int AuthorId, string AuthorName);

public record AuthorInfo(int AuthorId, string AuthorCode, string AuthorName, IEnumerable<BookResponseDto> Books);