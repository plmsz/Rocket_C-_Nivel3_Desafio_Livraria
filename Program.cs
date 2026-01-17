using Nivel3_Desafio_Livraria.Infrastucture;
using Nivel3_Desafio_Livraria.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<BooksRepository>(); // DI - uma unica instancia para exita pra
                                                  // toda a a vida da aplicação
builder.Services.AddSingleton<BookService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();