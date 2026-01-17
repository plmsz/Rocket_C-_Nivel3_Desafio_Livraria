using Nivel3_Desafio_Livraria.Enums;
using System.Text.Json.Serialization;

namespace Nivel3_Desafio_Livraria.Comunnication;

public class ResponseCreateBookJson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }

    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
}