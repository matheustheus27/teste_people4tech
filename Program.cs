using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using teste_people4tech.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() {Title = "TestePeople4Tech API", Version = "v1"});

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblies(System.Reflection.Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "doc/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/doc/v1/swagger.json", "TestePeople4Tech API v1");
    c.RoutePrefix = "doc";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
