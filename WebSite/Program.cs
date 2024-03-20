using Application.Services;
using Application.UnitOfWork;
using Domain.IRepositories;
using Infrastructure.Repositories;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
//Database
string Connection = builder.Configuration.GetConnectionString("sqlServer");
builder.Services.AddSqlServer<DbContextEF>(Connection);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, DbContextEF>();
//User
builder.Services.AddScoped<IUserRepositorie , UserRepositorie>();
builder.Services.AddScoped<UserService>();
//BankAccount
builder.Services.AddScoped<IBankAccountRepositorie, BankAccountRepositorie>();
builder.Services.AddScoped<BankAccountService>();

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
