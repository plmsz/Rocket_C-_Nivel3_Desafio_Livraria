using Microsoft.AspNetCore.Mvc;
using Nivel3_Desafio_Livraria.Comunnication;
using Nivel3_Desafio_Livraria.Entities;
using Nivel3_Desafio_Livraria.Infrastucture;
using Nivel3_Desafio_Livraria.Services;

namespace Nivel3_Desafio_Livraria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BooksRepository _repository; //recuperar e modificar no controller o inventário de livros
    private readonly BookService _bookService;
    public BooksController(BooksRepository repository, BookService bookService) // .net injetar automaticamente aqui
    {
        _repository = repository;
        _bookService = bookService;
    }

    [ProducesResponseType(typeof(ResponseCreateBookJson), 200)]
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_repository.Inventory.ToList());
    }

    [ProducesResponseType(typeof(ResponseCreateBookJson[]), 200)]
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        var book = _repository.Inventory.Find(book => book.Id == id);

        if (book is null)
        {
            return NotFound("Book was not found");
        }

        return Ok(book);
    }

    [ProducesResponseType(typeof(ResponseCreateBookJson), 201)]
    [ProducesResponseType(typeof(string), 404)]
    [HttpPost]
    public IActionResult Create([FromBody] RequestBookJson payload)
    {
        try
        {
            _bookService.ValidateBook(payload);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var book = new Book
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Title = payload.Title,
            Author = payload.Author,
            Genre = payload.Genre,
            Price = payload.Price,
            Stock = payload.Stock,
        };

        _repository.Inventory.Add(book);

        return Created(string.Empty, book);
    }

    [ProducesResponseType(typeof(ResponseCreateBookJson), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 409)]
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, RequestBookJson payload)
    {
        var bookRepository = _repository.Inventory.Find(book => book.Id == id);

        if (bookRepository is null)
        {
            return NotFound("Book was not found");
        }

        try
        {
            _bookService.ValidateBook(payload, id);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }

        var book = new Book
        {
            Id = bookRepository.Id,
            Title = payload.Title,
            Author = payload.Author,
            Genre = payload.Genre,
            Price = payload.Price,
            Stock = payload.Stock,
            CreatedAt = bookRepository.CreatedAt,
            UpdatedAt = DateTime.UtcNow,
        };

        _repository.Inventory.Remove(bookRepository);

        _repository.Inventory.Add(book);

        return Ok(book);
    }

    [ProducesResponseType(typeof(string), 204)]
    [ProducesResponseType(typeof(string), 404)]
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var bookRepository = _repository.Inventory.Find(book => book.Id == id);

        if (bookRepository is null)
        {
            return NotFound("Book was not found");
        }

        _repository.Inventory.Remove(bookRepository);

        return NoContent();
    }
}