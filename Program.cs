using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MinimalCrud.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração de injeção de dependência do DbContext para operações no banco de dados.
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=database.db"));

// Configuração para não exibir na response campos cujos valores são nulos.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

// Endpoint para "health check" da API para checar se está "rodando".
app.MapGet("/", () => new { message = "API is running :)" });

// GET - listar contatos
app.MapGet("/contacts", async (DatabaseContext db) =>
    await db.Contacts.ToListAsync());

// GET BY ID - exibir informações de um contato específico.
app.MapGet("/contacts/{id:int}", async (int id, DatabaseContext db) =>
    await db.Contacts.FindAsync(id) is Contact contact ? Results.Ok(contact) : Results.NotFound("Contact not found"));

// POST - criar novo registro no banco de dados.
app.MapPost("/contacts", async (Contact contact, DatabaseContext db) =>
{
    db.Contacts.Add(contact);
    await db.SaveChangesAsync();
    return Results.Created($"/contacts/{contact.Id}", contact);
});

// PUT - atualizar informações de um contato existente
app.MapPut("/contacts/{id:int}", async (int id, Contact updatedContact, DatabaseContext db) =>
{
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null) return Results.NotFound("Contact not found");

    contact.Name = updatedContact.Name;
    contact.Phone = updatedContact.Phone;
    contact.Email = updatedContact.Email;
    await db.SaveChangesAsync();

    return Results.Ok(contact);
});

// DELETE - excluir um contato do banco de dados.
app.MapDelete("/contacts/{id:int}", async (int id, DatabaseContext db) =>
{
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null) return Results.NotFound("Contact not found");

    db.Contacts.Remove(contact);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
