using DotNetCrudProject.Data;
using DotNetCrudProject.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => Results.Redirect("/swagger"));

// READ - merr te gjitha produktet
app.MapGet("/api/products", () =>
{
    return Results.Ok(ProductStore.GetAll());
});

// READ - merr nje produkt sipas ID
app.MapGet("/api/products/{id:int}", (int id) =>
{
    var product = ProductStore.GetById(id);
    return product is null ? Results.NotFound("Produkti nuk u gjet.") : Results.Ok(product);
});

// CREATE - shton nje produkt te ri
app.MapPost("/api/products", (CreateProductRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
        return Results.BadRequest("Emri i produktit eshte i detyrueshem.");

    if (request.Price < 0)
        return Results.BadRequest("Cmimi nuk mund te jete negativ.");

    var product = ProductStore.Create(request);
    return Results.Created($"/api/products/{product.Id}", product);
});

// UPDATE - perditeson nje produkt ekzistues
app.MapPut("/api/products/{id:int}", (int id, UpdateProductRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name))
        return Results.BadRequest("Emri i produktit eshte i detyrueshem.");

    if (request.Price < 0)
        return Results.BadRequest("Cmimi nuk mund te jete negativ.");

    var updatedProduct = ProductStore.Update(id, request);
    return updatedProduct is null ? Results.NotFound("Produkti nuk u gjet.") : Results.Ok(updatedProduct);
});

// DELETE - fshin nje produkt sipas ID
app.MapDelete("/api/products/{id:int}", (int id) =>
{
    var deleted = ProductStore.Delete(id);
    return deleted ? Results.NoContent() : Results.NotFound("Produkti nuk u gjet.");
});

app.Run();
