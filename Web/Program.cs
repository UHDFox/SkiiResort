using Application.Entities;
using Application.Infrastructure;
using Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddTransient<ISkipassService>(provider =>
{
    var service = provider.GetRequiredService<ISkipassService>();
    return new SkipassService(service);
});*/
//builder.Services.AddTransient<ISkipassService, SkipassService>();
//builder.Services.AddTransient<ISkipassService>(provider => provider.GetRequiredService<ISkipassService>());

/*builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Psql"));
});*/

builder.Services.ConfigureSkipassServices();
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