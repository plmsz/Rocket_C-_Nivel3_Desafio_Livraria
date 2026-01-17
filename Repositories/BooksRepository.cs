using Nivel3_Desafio_Livraria.Entities;

namespace Nivel3_Desafio_Livraria.Infrastucture;

public class BooksRepository
{
    public List<Book> Inventory { get; } = [];

    public BooksRepository()
    {
        Inventory.Add(new Book
        {
            Id = Guid.NewGuid(),
            Title = "Dom Casmurro",
            Author = "Machado de Assis",
            Genre = Enums.Genre.Romance,
            Price = 50.0m,
            Stock = 10,
            CreatedAt = DateTime.UtcNow
        });
    }
}