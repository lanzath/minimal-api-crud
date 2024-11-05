using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MinimalCrud.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source=database.db"));
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapGet("/", () => new { message = "API is running :)" });

app.MapGet("/contacts", async (DatabaseContext db) =>
    await db.Contacts.ToListAsync());

app.MapGet("/contacts/{id:int}", async (int id, DatabaseContext db) =>
    await db.Contacts.FindAsync(id) is Contact contact ? Results.Ok(contact) : Results.NotFound("Contact not found"));

app.MapPost("/contacts", async (Contact contact, DatabaseContext db) =>
{
    db.Contacts.Add(contact);
    await db.SaveChangesAsync();
    return Results.Created($"/contacts/{contact.Id}", contact);
});

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

app.MapDelete("/contacts/{id:int}", async (int id, DatabaseContext db) =>
{
    var contact = await db.Contacts.FindAsync(id);
    if (contact is null) return Results.NotFound("Contact not found");

    db.Contacts.Remove(contact);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
