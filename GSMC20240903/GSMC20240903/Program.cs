var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var products = new List<Productos>();



app.MapGet("/products", () =>
{
return products;
});


app.MapGet("/products/{id}", (int id) =>
{
var product = products.FirstOrDefault(c => c.Id == id);
return product;
});


app.MapPost("/products", (Productos product) =>
{
products.Add(product);
return Results.Ok();
});

app.MapPut("/products/{id}", (int id, Productos product) =>
{
var existingProduct = products.FirstOrDefault(c => c.Id == id);
if (existingProduct != null)
{
existingProduct.Nombre = product.Nombre;
existingProduct.Apellido = product.Apellido;
return Results.Ok();
}
else
{
return Results.NotFound();
}
});



app.MapDelete("/products/{id}", (int id) =>
{
var existingProduct = products.FirstOrDefault(c => c.Id == id);
if (existingProduct != null)
{
products.Remove(existingProduct);
return Results.Ok();
}
else
{
return Results.NotFound();
}
});

app.Run();

internal class Productos
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
}