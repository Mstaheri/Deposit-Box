
using Application.Services;
using Application.Services.Users.Commands.AddUser;
using Application.UnitOfWork;
using Domain.IRepositories;
using Domain.Validations;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
//Database
string Connection = builder.Configuration.GetConnectionString("sqlServer");
builder.Services.AddSqlServer<DbContextEF>(Connection);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, DbContextEF>();
builder.Services.AddScoped<IUserRepositorie , UserRepositorie>();
builder.Services.AddScoped<IBankAccountRepositorie, BankAccountRepositorie>();
builder.Services.AddScoped<IBankSafeRepositorie, BankSafeRepositorie>();
builder.Services.AddScoped<IUserAndNumberOfShareRepositorie, UserAndNumberOfShareRepositorie>();
builder.Services.AddScoped<IBankSafeTransactionsRepositorie, BankSafeTransactionsRepositorie>();
builder.Services.AddScoped<IBankSafeDocumentRepositorie, BankSafeDocumentRepositorie>();

builder.Services.RegisterApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
