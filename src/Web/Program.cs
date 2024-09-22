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
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("ApiCorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
