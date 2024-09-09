
using Application.Services;
using Application.Services.Users.Commands.AddUser;
using Application.UnitOfWork;
using Domain.IRepositories;
using Domain.IRepositories.IBankAccountRepositorie;
using Domain.IRepositories.IBankSafeDocumentRepositorie;
using Domain.IRepositories.IBankSafeRepositorie;
using Domain.IRepositories.IBankSafeTransactionsRepositorie;
using Domain.IRepositories.IChatRoomRepositorie;
using Domain.IRepositories.ILoanRepositorie;
using Domain.IRepositories.ISmsSevice;
using Domain.IRepositories.IUserAndNumberOfShareRepositorie;
using Domain.IRepositories.IUserRepositorie;
using Domain.Validations;
using HealthChecks.UI.Client;
using Infrastructure.Repositories;
using Infrastructure.Repositories.BankAccountRepositorie;
using Infrastructure.Repositories.BankSafeDocumentRepositorie;
using Infrastructure.Repositories.BankSafeRepositorie;
using Infrastructure.Repositories.BankSafeTransactionsRepositorie;
using Infrastructure.Repositories.ChatRoomRepositorie;
using Infrastructure.Repositories.LoanRepositorie;
using Infrastructure.Repositories.SmsServiceRepositorie;
using Infrastructure.Repositories.UserAndNumberOfShareRepositorie;
using Infrastructure.Repositories.UserRepositorie;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Prometheus;
using System.Net.Http;
using System.Reflection;
using System.Text;
using WebSite.Hubs;
using WebSite.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
//Database
string Connection = builder.Configuration.GetConnectionString("sqlServer");
builder.Services.AddSqlServer<DbContextEF>(Connection);

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("sqlServer"));

builder.Services.AddSignalR();


builder.Services.AddAuthentication(Option =>
{
    Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    Option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(ConfigOption =>
{
    ConfigOption.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["JWTConfig:issuer"],
        ValidAudience = builder.Configuration["JWTConfig:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Key"])),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
    ConfigOption.SaveToken = true;
    ConfigOption.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = context =>
        {
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            return Task.CompletedTask;
        },
    };
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, DbContextEF>();
builder.Services.AddScoped<IUserRepositorieCommand , UserRepositorieCommand>();
builder.Services.AddScoped<IUserRepositorieQuery, UserRepositorieQuery>();
builder.Services.AddScoped<IBankAccountRepositorieCommand, BankAccountRepositorieCommand>();
builder.Services.AddScoped<IBankAccountRepositorieQuery, BankAccountRepositorieQuery>();
builder.Services.AddScoped<IBankSafeRepositorieCommand, BankSafeRepositorieCommand>();
builder.Services.AddScoped<IBankSafeRepositorieQuery, BankSafeRepositorieQuery>();
builder.Services.AddScoped<IUserAndNumberOfShareRepositorieCommand, UserAndNumberOfShareRepositorieCommand>();
builder.Services.AddScoped<IUserAndNumberOfShareRepositorieQuery, UserAndNumberOfShareRepositorieQuery>();
builder.Services.AddScoped<IBankSafeTransactionsRepositorieCommand, BankSafeTransactionsRepositorieCommand>();
builder.Services.AddScoped<IBankSafeTransactionsRepositorieQuery, BankSafeTransactionsRepositorieQuery>();
builder.Services.AddScoped<IBankSafeDocumentRepositorieCommand, BankSafeDocumentRepositorieCommand>();
builder.Services.AddScoped<IBankSafeDocumentRepositorieQuery, BankSafeDocumentRepositorieQuery>();
builder.Services.AddScoped<ILoanRepositorieCommand, LoanRepositorieCommand>();
builder.Services.AddScoped<ILoanRepositorieQuery, LoanRepositorieQuery>();
builder.Services.AddScoped<IChatRoomRepositorieCommand, ChatRoomRepositorieCommand>();
builder.Services.AddScoped<IChatRoomRepositorieQuery, ChatRoomRepositorieQuery>();
builder.Services.AddScoped<ISmsServiceRepositorieQuery, SmsServiceRepositorieQuery>();
builder.Services.AddHttpClient();



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
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        endpoints.MapHub<SiteChatHub>("/chathub");
        endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
app.UseMetricServer();
app.UseHttpMetrics();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
