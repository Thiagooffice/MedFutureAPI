using FluentValidation.AspNetCore;
using MedFutureAPI.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// SQL Server
//var connectionString = builder.Configuration.GetConnectionString("UserAPICs");
//builder.Services.AddDbContext<UserDbContext>(
//    o => o.UseSqlServer("connectionString")
//    );

//Database in memory
builder.Services.AddDbContext<UserDbContext>(
    o => o.UseInMemoryDatabase("UserDb")
    );

builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
