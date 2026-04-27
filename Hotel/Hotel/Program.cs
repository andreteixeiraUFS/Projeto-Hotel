using Microsoft.EntityFrameworkCore;
using Hotel.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString
        ("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem
              .AllowAnyHeader()  // Permite qualquer cabeçalho
              .AllowAnyMethod(); // Permite qualquer método (GET, POST, etc.)
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// 2. Habilitar o Middleware CORS (deve ser antes de UseAuthorization)
app.UseCors("PermitirTudo");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
