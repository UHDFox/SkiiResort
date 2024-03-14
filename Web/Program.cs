using System.Reflection;
using Application.Infrastructure;
using Domain.Infrastructure;
using Repository.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSkiiResortContext();
builder.Services.AddBusinessServices();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddRepositories();

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