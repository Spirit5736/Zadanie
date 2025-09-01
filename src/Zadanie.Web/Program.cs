using Zadanie.Core.Interfaces;
using Zadanie.Infrastructure.Services;
using Zadanie.UseCases.Interfaces;
using Zadanie.UseCases.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IActionService, ActionService>();

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