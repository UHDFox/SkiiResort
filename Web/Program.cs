using Application.Infrastructure;
using Domain.Infrastructure;
using Repository.Infrastructure;
using Web.Infrastructure;
using ServiceCollectionExtension = Web.Infrastructure.ServiceCollectionExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

MvcServiceCollectionExtensions.AddControllers(builder.Services);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHotelContext();
builder.Services.AddBusinessServices();
builder.Services.AddAutomapper();
builder.Services.AddNewTonControllers();
builder.Services.AddRepositories();

builder.Services.AddSwaggerGen();

var app = builder.Build();
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