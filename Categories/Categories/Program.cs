using Categories.Models;
using Categories.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Category Hierarchy API",
        Version = "v1",
        Description = "API for managing category hierarchies with keyword inheritance and level queries"
    });
});

builder.Services.AddSingleton<ICategoryService>(_ => new CategoryService(new List<Category>
{
    new() { CategoryId = 100, ParentCategoryId = -1, Name = "Business", Keywords = "Money" },
    new() { CategoryId = 200, ParentCategoryId = -1, Name = "Tutoring", Keywords = "Teaching" },
    new() { CategoryId = 101, ParentCategoryId = 100, Name = "Accounting", Keywords = "Taxes" },
    new() { CategoryId = 102, ParentCategoryId = 100, Name = "Taxation", Keywords = "" },
    new() { CategoryId = 201, ParentCategoryId = 200, Name = "Computer", Keywords = "" },
    new() { CategoryId = 103, ParentCategoryId = 101, Name = "Corporate Tax", Keywords = "" },
    new() { CategoryId = 202, ParentCategoryId = 201, Name = "Operating System", Keywords = "" },
    new() { CategoryId = 109, ParentCategoryId = 101, Name = "Small Business Tax", Keywords = "" }
}));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Category v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
