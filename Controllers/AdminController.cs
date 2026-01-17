using Microsoft.AspNetCore.Mvc;
using Nivel3_Desafio_Livraria.Comunnication;
using Nivel3_Desafio_Livraria.Infrastucture;

namespace Nivel3_Desafio_Livraria.Controllers;

[Route("api/[controller]/books")]
[ApiController]
public sealed class AdminController : ProjectBaseController
{
    private readonly BooksRepository _repository; //recuperar e modificar no controller o inventário de livros

    protected override string Log()
    {
        return $"[ADMIN] {base.Log()} Timestamp: {DateTime.UtcNow}";
    }

    public AdminController(BooksRepository repository) // .net injetar automaticamente aqui
    {
        _repository = repository;
    }

    [ProducesResponseType(typeof(ResponseCreateBookJson), 200)]
    [HttpGet]
    [Route("report")]
    public IActionResult CreateReport([FromHeader] string key, [FromHeader] string token)
    {
        Console.WriteLine(Log());
        if (!ValideteKey(key))
        {
            return Unauthorized();
        }
        if (!ValidateToken(token))
        {
            return Unauthorized();
        }

        var soldOutBooks = _repository.Inventory.FindAll(b => b.Stock == 0);

        return Ok(soldOutBooks);
    }

    [ProducesResponseType(typeof(ResponseCreateBookJson[]), 200)]
    [HttpGet()]
    [Route("get-min-stock")]
    public IActionResult GetMinimumStock([FromQuery] int quantity, [FromHeader] string key)
    {
        Console.WriteLine(Log());
        if (!ValideteKey(key))
        {
            return Unauthorized();
        }

        var books = _repository.Inventory.FindAll(book => book.Stock >= quantity);

        if (books is null)
        {
            return NotFound("No books matched the minimum criteria");
        }
        
        return Ok(books);
    }
}