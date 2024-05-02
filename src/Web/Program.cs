using System.Reflection;
using SkiiResort.Application.Infrastructure;
using SkiiResort.Domain.Infrastructure;
using SkiiResort.Repository.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSkiiResortContext();
builder.Services.AddBusinessServices();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddRepositories();

builder.Services.AddCors(opts =>
    opts.AddPolicy("ApiCorsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:7045")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ApiCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
