


using BackCustomerManagement.Interfaces;
using BackCustomerManagement.Models;
using BackCustomerManagement.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();


builder.Services.AddScoped<IJsonFileService>(provider =>
    new JsonFileService(
        "C:\\Git\\CustomerManagement\\DB\\data.json",
        provider.GetRequiredService<IPasswordHasher<Customer>>()));



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000") 
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
