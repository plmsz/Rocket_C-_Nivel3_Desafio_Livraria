using Nivel3_Desafio_Livraria.Enums;

namespace Nivel3_Desafio_Livraria.Comunnication;

public class RequestUpdateBookJson
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}