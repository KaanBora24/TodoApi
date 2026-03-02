using System.Collections.Generic;
using OpenAI.Chat;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi;


var builder = WebApplication.CreateBuilder(args);
var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
if (string.IsNullOrWhiteSpace(apiKey))
{
    throw new Exception("OPENAI_API_KEY bulunamadı. Environment variable ayarlı mı?");
}

builder.Services.AddSingleton(new ChatClient(
    model: "gpt-5-mini",
    apiKey: apiKey
));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
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
