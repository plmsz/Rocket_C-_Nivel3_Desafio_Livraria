using Nivel3_Desafio_Livraria.Comunnication;
using Nivel3_Desafio_Livraria.Enums;
using Nivel3_Desafio_Livraria.Infrastucture;

namespace Nivel3_Desafio_Livraria.Services;

public class BookService
{
    private readonly BooksRepository _repository;

    public BookService(BooksRepository repository)
    {
        _repository = repository;
    }

    public void ValidateBook(RequestBookJson book, Guid? id = null)
    {
        var isDuplicate = _repository.Inventory.Exists(b => (b.Title == book.Title) && b.Author == book.Author && b.Id != id);

        if (isDuplicate)
        {
            throw new InvalidOperationException("Book is duplicated.");
        }

        var isGenreValid = Enum.IsDefined(typeof(Genre), book.Genre);

        if (!isGenreValid)
        {
            throw new ArgumentException("Genre is invalid");
        }

        var isPriceValid = book.Price > 0;

        if (!isPriceValid)
        {
            throw new ArgumentException("Price is invalid.");
        }

        var hasStock = book.Stock >= 0;
        if (!hasStock)
        {
            throw new ArgumentException("Stock must be greater than 0.");
        }

        var isTitleValid = book.Title.Length >= 2 && book.Title.Length <= 120;
        if (!isTitleValid)
        {
            throw new ArgumentException("Title must be between 2 and 12 characters.");
        }

        var isAuthorValid = book.Author.Length >= 2 && book.Author.Length <= 120;

        if (!isAuthorValid)
        {
            throw new ArgumentException("Author must be between 2 and 12 characters.");
        }
    }
}